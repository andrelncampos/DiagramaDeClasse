namespace DiagramaDeClasse.Modelos;

public class TipoDiagrama
{
    public string Nome { get; set; } = string.Empty;
    public string CodigoExemplo { get; set; } = string.Empty;

    public static List<TipoDiagrama> ObterTodos() => new()
    {
        new TipoDiagrama
        {
            Nome = "Diagrama de Classe",
            CodigoExemplo = @"classDiagram
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
    Garagem ""1"" o-- ""0..*"" Carro : abriga"
        },
        new TipoDiagrama
        {
            Nome = "Fluxograma",
            CodigoExemplo = @"flowchart TD
    A[Início] --> B{Decisão}
    B -->|Sim| C[Processo 1]
    B -->|Não| D[Processo 2]
    C --> E[Fim]
    D --> E"
        },
        new TipoDiagrama
        {
            Nome = "Diagrama de Sequência",
            CodigoExemplo = @"sequenceDiagram
    participant Cliente
    participant Servidor
    participant BancoDados
    
    Cliente->>Servidor: Requisição
    Servidor->>BancoDados: Consulta
    BancoDados-->>Servidor: Resultado
    Servidor-->>Cliente: Resposta"
        },
        new TipoDiagrama
        {
            Nome = "Diagrama de Estado",
            CodigoExemplo = @"stateDiagram-v2
    [*] --> Inativo
    Inativo --> Ativo: iniciar
    Ativo --> Pausado: pausar
    Pausado --> Ativo: retomar
    Ativo --> Finalizado: concluir
    Finalizado --> [*]"
        },
        new TipoDiagrama
        {
            Nome = "Diagrama ER",
            CodigoExemplo = @"erDiagram
    CLIENTE ||--o{ PEDIDO : faz
    PEDIDO ||--|{ ITEM_PEDIDO : contem
    PRODUTO ||--o{ ITEM_PEDIDO : esta_em
    
    CLIENTE {
        int id
        string nome
        string email
    }
    
    PEDIDO {
        int id
        date data
        decimal total
    }
    
    PRODUTO {
        int id
        string nome
        decimal preco
    }"
        },
        new TipoDiagrama
        {
            Nome = "Gráfico de Gantt",
            CodigoExemplo = @"gantt
    title Cronograma do Projeto
    dateFormat YYYY-MM-DD
    
    section Planejamento
    Análise           :a1, 2024-01-01, 7d
    Design            :a2, after a1, 5d
    
    section Desenvolvimento
    Backend           :b1, after a2, 10d
    Frontend          :b2, after a2, 10d
    
    section Testes
    Testes            :c1, after b1, 5d"
        },
        new TipoDiagrama
        {
            Nome = "Gráfico de Pizza",
            CodigoExemplo = @"pie title Distribuição de Vendas
    ""Produto A"" : 45
    ""Produto B"" : 30
    ""Produto C"" : 15
    ""Produto D"" : 10"
        },
        new TipoDiagrama
        {
            Nome = "Jornada do Usuário",
            CodigoExemplo = @"journey
    title Jornada de Compra Online
    section Descoberta
      Pesquisar produto: 5: Cliente
      Ver detalhes: 4: Cliente
    section Decisão
      Comparar preços: 3: Cliente
      Adicionar ao carrinho: 5: Cliente
    section Compra
      Finalizar pedido: 4: Cliente
      Pagamento: 3: Cliente, Sistema
      Confirmação: 5: Cliente, Sistema"
        },
        new TipoDiagrama
        {
            Nome = "Diagrama Git",
            CodigoExemplo = @"gitGraph
    commit
    commit
    branch develop
    checkout develop
    commit
    commit
    checkout main
    merge develop
    commit"
        },
        new TipoDiagrama
        {
            Nome = "Mindmap",
            CodigoExemplo = @"mindmap
  root((Projeto))
    Planejamento
      Requisitos
      Cronograma
    Desenvolvimento
      Backend
      Frontend
      Banco de Dados
    Testes
      Unitários
      Integração
    Deploy
      Produção
      Monitoramento"
        },
        new TipoDiagrama
        {
            Nome = "Timeline",
            CodigoExemplo = @"timeline
    title História da Tecnologia
    2000 : Início da Internet Banda Larga
    2007 : Lançamento do iPhone
    2010 : Popularização das Redes Sociais
    2015 : Crescimento da Computação em Nuvem
    2020 : Expansão do Trabalho Remoto
    2023 : Era da Inteligência Artificial"
        }
    };
}
