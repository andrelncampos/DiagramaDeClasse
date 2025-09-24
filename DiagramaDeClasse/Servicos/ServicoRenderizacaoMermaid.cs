using Microsoft.JSInterop;

namespace DiagramaDeClasse.Servicos;

public class ServicoRenderizacaoMermaid
{
    private readonly IJSRuntime _jsRuntime;

    public ServicoRenderizacaoMermaid(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task RenderizarDiagramaAsync(string elementoId, string codigoMermaid)
    {
        if (string.IsNullOrWhiteSpace(codigoMermaid))
            return;

        try
        {
            await _jsRuntime.InvokeAsync<bool>("mermaidInterop.render", elementoId, codigoMermaid);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao renderizar diagrama: {ex.Message}");
            throw;
        }
    }
}