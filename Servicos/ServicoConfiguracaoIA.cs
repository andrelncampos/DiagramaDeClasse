using DiagramaDeClasse.Modelos;
using Microsoft.JSInterop;
using System.Text.Json;

namespace DiagramaDeClasse.Servicos;

public class ServicoConfiguracaoIA
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ServicoCriptografia _servicoCriptografia;
    private const string ChaveArmazenamento = "configuracaoIA";

    public ServicoConfiguracaoIA(IJSRuntime jsRuntime, ServicoCriptografia servicoCriptografia)
    {
        _jsRuntime = jsRuntime;
        _servicoCriptografia = servicoCriptografia;
    }

    public async Task SalvarConfiguracaoAsync(ConfiguracaoIA configuracao)
    {
        var json = JsonSerializer.Serialize(configuracao);
        var jsonCriptografado = await _servicoCriptografia.CriptografarAsync(json);
        await _jsRuntime.InvokeVoidAsync("mermaidInterop.saveToLocalStorage", ChaveArmazenamento, jsonCriptografado);
    }

    public async Task<ConfiguracaoIA> CarregarConfiguracaoAsync()
    {
        var jsonCriptografado = await _jsRuntime.InvokeAsync<string>("mermaidInterop.readFromLocalStorage", ChaveArmazenamento);
        
        if (string.IsNullOrEmpty(jsonCriptografado))
            return new ConfiguracaoIA();

        var json = await _servicoCriptografia.DescriptografarAsync(jsonCriptografado);
        
        if (string.IsNullOrEmpty(json))
            return new ConfiguracaoIA();

        return JsonSerializer.Deserialize<ConfiguracaoIA>(json) ?? new ConfiguracaoIA();
    }
}
