# Editor de Diagramas de Classe

Aplicação Blazor WebAssembly para criação e edição de diagramas de classe usando sintaxe Mermaid.js.

## Como Contribuir

### Pré-requisitos
- .NET 9.0 SDK
- Visual Studio 2022 ou VS Code
- Conhecimento básico em Blazor WebAssembly
- Conhecimento básico em MudBlazor
- Conhecimento básico em JavaScript

### Executando o Projeto
```bash
dotnet restore
dotnet run
```

## Arquitetura e Padrões

### Princípios de Design
- **SOLID**: Cada classe tem responsabilidade única, extensível via herança
- **Clean Code**: Nomes em português, métodos pequenos, baixo acoplamento
- **Componentização**: UI dividida em componentes reutilizáveis
- **Abstração**: Preferência por classes abstratas sobre interfaces

### Estrutura do Código

```
├── Componentes/           # Componentes UI reutilizáveis
│   ├── BarraFerramentas.razor    # Botões salvar/carregar
│   ├── EditorCodigo.razor        # Editor de texto Mermaid
│   └── VisualizadorDiagrama.razor # Renderização do diagrama
├── Modelos/              # Entidades de domínio
│   └── DiagramaMermaid.cs        # Modelo do diagrama
├── Servicos/             # Lógica de negócio
│   ├── ServicoArmazenamentoBase.cs    # Classe abstrata base
│   ├── ServicoArmazenamentoLocal.cs   # Implementação LocalStorage
│   └── ServicoRenderizacaoMermaid.cs  # Integração Mermaid.js
├── Pages/                # Páginas da aplicação
│   └── DiagramEditor.razor       # Página principal
└── Layout/               # Layout base
    └── MainLayout.razor          # Template da aplicação
```

### Padrões Implementados

**1. Template Method (ServicoArmazenamentoBase)**
- Define algoritmo base para armazenamento
- Subclasses implementam detalhes específicos

**2. Dependency Injection**
- Serviços registrados no Program.cs
- Componentes recebem dependências via @inject

**3. Component Pattern**
- Cada componente tem responsabilidade específica
- Comunicação via parâmetros e callbacks

## Como Adicionar Funcionalidades

### Novo Tipo de Armazenamento
1. Herde de `ServicoArmazenamentoBase`
2. Implemente `SalvarAsync` e `CarregarAsync`
3. Registre no DI container

### Novo Componente UI
1. Crie arquivo .razor em `Componentes/`
2. Defina parâmetros com `[Parameter]`
3. Use no componente pai

### Nova Funcionalidade de Diagrama
1. Estenda `DiagramaMermaid` se necessário
2. Adicione método em `ServicoRenderizacaoMermaid`
3. Atualize componentes UI relacionados

## Tecnologias Utilizadas

- **Blazor WebAssembly**: Framework UI
- **MudBlazor**: Biblioteca de componentes
- **Mermaid.js**: Renderização de diagramas
- **LocalStorage**: Persistência local

## Funcionalidades Atuais

- ✅ Edição de código Mermaid em tempo real
- ✅ Visualização instantânea de diagramas
- ✅ Salvamento/carregamento via LocalStorage
- ✅ Exportação para SVG
- ✅ Interface responsiva com MudBlazor
- ✅ Arquitetura extensível e testável

## Roadmap de Melhorias

- 🔄 Exportação para PNG
- 🔄 Múltiplos diagramas em abas
- 🔄 Armazenamento em nuvem
- 🔄 Colaboração em tempo real
- 🔄 Templates de diagramas