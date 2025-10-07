namespace DiagramaDeClasse.Modelos;

public class ConfiguracaoIA
{
    public ProvedorIA Provedor { get; set; } = ProvedorIA.OpenAI;
    public string ChaveApi { get; set; } = string.Empty;
    public string UrlBase { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;

    public bool EstaConfigurada => !string.IsNullOrWhiteSpace(ChaveApi);
}

public enum ProvedorIA
{
    OpenAI,
    Google,
    Claude
}
