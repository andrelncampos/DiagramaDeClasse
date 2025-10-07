using DiagramaDeClasse.Modelos;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiagramaDeClasse.Servicos;

public class ServicoIA
{
    private readonly HttpClient _httpClient;
    private readonly ServicoConfiguracaoIA _servicoConfiguracao;

    public ServicoIA(HttpClient httpClient, ServicoConfiguracaoIA servicoConfiguracao)
    {
        _httpClient = httpClient;
        _servicoConfiguracao = servicoConfiguracao;
    }

    public async Task<string> GerarDiagramaAsync(string diagramaAtual, string prompt, string tipoDiagrama)
    {
        var config = await _servicoConfiguracao.CarregarConfiguracaoAsync();
        
        if (!config.EstaConfigurada)
            throw new InvalidOperationException("Configure a IA antes de usar");

        var promptCompleto = ConstruirPrompt(diagramaAtual, prompt, tipoDiagrama);
        
        Console.WriteLine("=== PROMPT ENVIADO PARA IA ===");
        Console.WriteLine(promptCompleto);
        Console.WriteLine("===============================");

        return config.Provedor switch
        {
            ProvedorIA.OpenAI => await ChamarOpenAIAsync(config, promptCompleto),
            ProvedorIA.Google => await ChamarGoogleAsync(config, promptCompleto),
            ProvedorIA.Claude => await ChamarClaudeAsync(config, promptCompleto),
            _ => throw new NotSupportedException($"Provedor {config.Provedor} não suportado")
        };
    }

    private string ConstruirPrompt(string diagramaAtual, string prompt, string tipoDiagrama)
    {
        var sintaxeInicial = ObterSintaxeInicial(tipoDiagrama);
        
        var promptBase = $@"Você é um especialista em diagramas Mermaid. Sua tarefa é gerar ou modificar um {tipoDiagrama}.

REGRAS IMPORTANTES:
1. Retorne APENAS o código Mermaid puro, sem explicações, sem markdown, sem ```mermaid
2. O diagrama DEVE ser do tipo: {tipoDiagrama}
3. SEMPRE comece com: {sintaxeInicial}
4. Use sintaxe válida do Mermaid para {tipoDiagrama}
5. NÃO crie outro tipo de diagrama, apenas {tipoDiagrama}

";

        if (!string.IsNullOrWhiteSpace(diagramaAtual))
        {
            promptBase += $@"DIAGRAMA ATUAL:
{diagramaAtual}

SOLICITAÇÃO DO USUÁRIO:
{prompt}

Modifique o diagrama acima conforme solicitado, mantendo o tipo {tipoDiagrama}.

IMPORTANTE: O Mermaid de retorno deve ser do tipo {sintaxeInicial} ({tipoDiagrama}).";
        }
        else
        {
            promptBase += $@"SOLICITAÇÃO DO USUÁRIO:
{prompt}

Crie um novo {tipoDiagrama} conforme solicitado.

IMPORTANTE: O Mermaid de retorno deve ser do tipo {sintaxeInicial} ({tipoDiagrama}). Comece com {sintaxeInicial}";
        }

        return promptBase;
    }

    private string ObterSintaxeInicial(string tipoDiagrama)
    {
        return tipoDiagrama.ToLower() switch
        {
            "diagrama de classe" => "classDiagram",
            "fluxograma" => "flowchart TD",
            "diagrama de sequência" => "sequenceDiagram",
            "diagrama de estado" => "stateDiagram-v2",
            "diagrama er" => "erDiagram",
            "gráfico de gantt" => "gantt",
            "diagrama de jornada do usuário" => "journey",
            "gráfico de pizza" => "pie",
            _ => "graph TD"
        };
    }

    private async Task<string> ChamarOpenAIAsync(ConfiguracaoIA config, string prompt)
    {
        var request = new
        {
            model = config.Modelo,
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            temperature = 0.7
        };

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{config.UrlBase}/chat/completions");
        httpRequest.Headers.Add("Authorization", $"Bearer {config.ChaveApi}");
        httpRequest.Content = JsonContent.Create(request);

        var response = await _httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
        return ExtrairMermaid(result?.Choices?[0]?.Message?.Content ?? string.Empty);
    }

    private async Task<string> ChamarGoogleAsync(ConfiguracaoIA config, string prompt)
    {
        var request = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[] { new { text = prompt } }
                }
            }
        };

        var url = $"{config.UrlBase.TrimEnd('/')}/v1beta/models/{config.Modelo}:generateContent?key={config.ChaveApi}";
        var response = await _httpClient.PostAsJsonAsync(url, request);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro na API do Google: {response.ReasonPhrase} - {errorContent}");
        }

        var result = await response.Content.ReadFromJsonAsync<GoogleResponse>();
        return ExtrairMermaid(result?.Candidates?[0]?.Content?.Parts?[0]?.Text ?? string.Empty);
    }

    private async Task<string> ChamarClaudeAsync(ConfiguracaoIA config, string prompt)
    {
        var request = new
        {
            model = config.Modelo,
            max_tokens = 4096,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{config.UrlBase}/messages");
        httpRequest.Headers.Add("x-api-key", config.ChaveApi);
        httpRequest.Headers.Add("anthropic-version", "2023-06-01");
        httpRequest.Content = JsonContent.Create(request);

        var response = await _httpClient.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ClaudeResponse>();
        return ExtrairMermaid(result?.Content?[0]?.Text ?? string.Empty);
    }

    private string ExtrairMermaid(string resposta)
    {
        if (string.IsNullOrWhiteSpace(resposta))
            return string.Empty;

        // Remove markdown code blocks se existirem
        resposta = resposta.Trim();
        if (resposta.StartsWith("```mermaid"))
            resposta = resposta.Substring(10);
        else if (resposta.StartsWith("```"))
            resposta = resposta.Substring(3);

        if (resposta.EndsWith("```"))
            resposta = resposta.Substring(0, resposta.Length - 3);

        return resposta.Trim();
    }

    private class OpenAIResponse
    {
        [JsonPropertyName("choices")]
        public Choice[]? Choices { get; set; }

        public class Choice
        {
            [JsonPropertyName("message")]
            public Message? Message { get; set; }
        }

        public class Message
        {
            [JsonPropertyName("content")]
            public string? Content { get; set; }
        }
    }

    private class GoogleResponse
    {
        [JsonPropertyName("candidates")]
        public Candidate[]? Candidates { get; set; }

        public class Candidate
        {
            [JsonPropertyName("content")]
            public Content? Content { get; set; }
        }

        public class Content
        {
            [JsonPropertyName("parts")]
            public Part[]? Parts { get; set; }
        }

        public class Part
        {
            [JsonPropertyName("text")]
            public string? Text { get; set; }
        }
    }

    private class ClaudeResponse
    {
        [JsonPropertyName("content")]
        public ClaudeContent[]? Content { get; set; }

        public class ClaudeContent
        {
            [JsonPropertyName("text")]
            public string? Text { get; set; }
        }
    }
}
