namespace DiagramaDeClasse.Modelos;

public class DiagramaMermaid
{
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = "Novo Diagrama";
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime DataModificacao { get; set; } = DateTime.Now;

    public bool EstaVazio => string.IsNullOrWhiteSpace(Codigo);

    public void AtualizarCodigo(string novoCodigo)
    {
        Codigo = novoCodigo ?? string.Empty;
        DataModificacao = DateTime.Now;
    }
}