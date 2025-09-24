using DiagramaDeClasse.Modelos;

namespace DiagramaDeClasse.Servicos;

public abstract class ServicoArmazenamentoBase
{
    protected const string ChaveArmazenamento = "diagramaMermaidCodigo";

    public abstract Task SalvarDiagramaAsync(DiagramaMermaid diagrama);
    public abstract Task<DiagramaMermaid?> CarregarDiagramaAsync();

    protected virtual DiagramaMermaid CriarDiagramaPadrao()
    {
        var diagrama = new DiagramaMermaid();
        var codigoExemplo = @"classDiagram
    direction LR

    class Veiculo {
      #String marca
      #String modelo
      +acelerar()
      +frear()
    }

    class Motor {
      -int potenciaHP
    }

    class Carro {
      -int numeroDePortas
      +abrirPortaMalas()
    }
    
    class Motocicleta {
      -int cilindradas
      +empinar()
    }

    class Motorista {
      -String nome
      -String cnh
    }

    class Garagem {
        -String endereco
    }

    Veiculo <|-- Carro
    Veiculo <|-- Motocicleta
    Veiculo ""1"" *-- ""1"" Motor : possui
    Motorista ""1"" --> ""0..*"" Carro : dirige
    Garagem ""1"" o-- ""0..*"" Carro : abriga";
        diagrama.AtualizarCodigo(codigoExemplo);
        return diagrama;
    }
}