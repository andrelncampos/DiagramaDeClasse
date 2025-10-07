using DiagramaDeClasse.Modelos;
using Microsoft.JSInterop;

namespace DiagramaDeClasse.Servicos;

public class ServicoArmazenamentoLocal : ServicoArmazenamentoBase
{
    private readonly IJSRuntime _jsRuntime;

    public ServicoArmazenamentoLocal(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task SalvarDiagramaAsync(DiagramaMermaid diagrama)
    {
        await _jsRuntime.InvokeVoidAsync("mermaidInterop.saveToLocalStorage", ChaveArmazenamento, diagrama.Codigo);
    }

    public override async Task<DiagramaMermaid?> CarregarDiagramaAsync()
    {
        var codigoSalvo = await _jsRuntime.InvokeAsync<string>("mermaidInterop.readFromLocalStorage", ChaveArmazenamento);
        
        if (string.IsNullOrEmpty(codigoSalvo))
            return CriarDiagramaPadrao();

        var diagrama = new DiagramaMermaid();
        diagrama.AtualizarCodigo(codigoSalvo);
        return diagrama;
    }
    
    public async Task SalvarDiagramaPorTipoAsync(string tipoDiagrama, DiagramaMermaid diagrama)
    {
        var chave = $"{ChaveArmazenamento}_{tipoDiagrama}";
        await _jsRuntime.InvokeVoidAsync("mermaidInterop.saveToLocalStorage", chave, diagrama.Codigo);
    }
    
    public async Task<DiagramaMermaid?> CarregarDiagramaPorTipoAsync(string tipoDiagrama)
    {
        var chave = $"{ChaveArmazenamento}_{tipoDiagrama}";
        var codigoSalvo = await _jsRuntime.InvokeAsync<string>("mermaidInterop.readFromLocalStorage", chave);
        
        if (string.IsNullOrEmpty(codigoSalvo))
            return null;

        var diagrama = new DiagramaMermaid();
        diagrama.AtualizarCodigo(codigoSalvo);
        return diagrama;
    }
}