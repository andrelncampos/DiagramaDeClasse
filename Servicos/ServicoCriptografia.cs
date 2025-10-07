using Microsoft.JSInterop;

namespace DiagramaDeClasse.Servicos;

public class ServicoCriptografia
{
    private readonly IJSRuntime _jsRuntime;

    public ServicoCriptografia(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> CriptografarAsync(string texto)
    {
        return await _jsRuntime.InvokeAsync<string>("cryptoInterop.encrypt", texto);
    }

    public async Task<string?> DescriptografarAsync(string textoCriptografado)
    {
        return await _jsRuntime.InvokeAsync<string?>("cryptoInterop.decrypt", textoCriptografado);
    }
}
