PROMPT PARA AMAZON Q (Refatoração de README.md)
Função e Objetivo:

Atue como um Engenheiro de Software Sênior e um Developer Advocate experiente em projetos de código aberto. Seu objetivo é refatorar um arquivo README.md existente para torná-lo mais eficaz em atrair, orientar e engajar novos contribuidores. A nova estrutura deve ser mais acolhedora, seguir as melhores práticas de projetos open source e organizar a informação de forma lógica para diferentes públicos (usuários, contribuidores e desenvolvedores).

Contexto (README Atual):

O projeto Editor de Diagramas de Classe possui o seguinte arquivo README.md:

Markdown

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
Arquitetura e Padrões
... (resto do conteúdo original) ...


**Análise Crítica do README Atual:**

* **Conteúdo Técnico Rico:** O README atual é excelente como um documento de arquitetura para a equipe interna.
* **Estrutura Pouco Acolhedora:** Ele começa com pré-requisitos técnicos, o que pode intimidar novos usuários ou contribuidores casuais.
* **Foco Imediato no "Como":** A seção "Como Contribuir" explica os pré-requisitos técnicos, mas não o **processo de colaboração** (Issues, Forks, Pull Requests), que é a informação mais importante para um novo contribuidor.
* **Ausência de Apelo Visual:** Falta uma imagem ou GIF que mostre a aplicação funcionando, o que é fundamental para capturar o interesse inicial.
* **Falta de Seções Padrão:** Não há menção à licença do projeto ou a um código de conduta.

**Instruções Detalhadas para a Refatoração:**

Reescreva e reestruture o `README.md` seguindo o layout e as diretrizes abaixo. Mova o conteúdo existente para as novas seções apropriadas.

---

**(Início do Novo README.md)**

# Editor de Diagramas de Classe

[![Status da Build](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/seu-usuario/seu-projeto/actions)
[![Licença: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Contribuições: Bem-vindas](https://img.shields.io/badge/contributions-welcome-orange.svg)](#como-contribuir)

Uma ferramenta web moderna, de código aberto, construída com Blazor WebAssembly para criar, editar e visualizar diagramas de classe em tempo real usando a sintaxe intuitiva do Mermaid.js.

**(Placeholder para um GIF/Screenshot da aplicação em funcionamento)**
> Ex: `![Screenshot da Ferramenta](link_para_sua_imagem.gif)`

## ✨ Funcionalidades Principais

* ✅ Edição de código Mermaid em tempo real
* ✅ Visualização instantânea de diagramas
* ✅ Divisor de painel ajustável para otimizar seu espaço de trabalho
* ✅ Salvamento e carregamento do seu trabalho no navegador (LocalStorage)
* ✅ Exportação do diagrama para SVG
* ✅ Interface moderna e responsiva com MudBlazor

## 🚀 Como Executar Localmente (Getting Started)

Para executar o projeto na sua máquina, siga os passos abaixo.

**Pré-requisitos:**
* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

**Passos:**
1.  Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/seu-projeto.git
    cd seu-projeto
    ```
2.  Restaure as dependências e execute a aplicação:
    ```bash
    dotnet restore
    dotnet run
    ```
3.  Abra seu navegador e acesse `http://localhost:5000` (ou a porta indicada no terminal).

## 🤝 Como Contribuir

Contribuições são extremamente bem-vindas! Este projeto existe graças a pessoas como você. Se você tem uma ideia, uma sugestão ou encontrou um bug, por favor, não hesite em colaborar.

Confira nosso **[Guia de Contribuição](CONTRIBUTING.md)** para mais detalhes sobre nosso fluxo de trabalho e padrões de código, e nosso **[Código de Conduta](CODE_OF_CONDUCT.md)** para entender os padrões da nossa comunidade.

O processo geral é:
1.  **Abra uma Issue:** Discuta a mudança que você quer fazer abrindo uma issue.
2.  **Faça o Fork:** Crie uma cópia do repositório na sua conta.
3.  **Crie uma Branch:** Crie uma branch descritiva para sua alteração.
4.  **Envie um Pull Request:** Envie suas alterações para revisão.

## 🛠️ Tecnologias Utilizadas

* **Blazor WebAssembly**: Framework UI principal
* **MudBlazor**: Biblioteca de componentes Material Design
* **Mermaid.js**: Biblioteca para renderização de diagramas a partir de texto
* **LocalStorage**: API do navegador para persistência local de dados

## 🗺️ Roadmap de Melhorias

Aqui estão algumas das funcionalidades que planejamos para o futuro. Sinta-se à vontade para pegar uma delas e contribuir!

* 🔄 Exportação para PNG e PDF
* 🔄 Múltiplos diagramas em abas
* 🔄 Armazenamento em nuvem (ex: GitHub Gist)
* 🔄 Colaboração em tempo real
* 🔄 Galeria de templates de diagramas

## 🏛️ Detalhes da Arquitetura (Para Desenvolvedores)

<details>
<summary>Clique para expandir e ver os detalhes técnicos do projeto</summary>

### Princípios de Design
- **SOLID**: Cada classe tem responsabilidade única, é extensível e favorece a injeção de dependência.
- **Clean Code**: O código é escrito em português, com métodos pequenos e baixo acoplamento para facilitar a leitura e manutenção.
- **Componentização**: A interface do usuário é dividida em componentes Blazor reutilizáveis e com responsabilidades bem definidas.

### Estrutura do Código
├── Componentes/              # Componentes UI reutilizáveis
│   ├── BarraFerramentas.razor      # Botões salvar/carregar
│   ├── EditorCodigo.razor          # Editor de texto Mermaid
│   └── VisualizadorDiagrama.razor  # Renderização do diagrama
├── Modelos/                  # Entidades de domínio
│   └── DiagramaMermaid.cs          # Modelo do diagrama
├── Servicos/                 # Lógica de negócio e abstrações
│   ├── ServicoArmazenamentoBase.cs   # Classe abstrata base
│   ├── ServicoArmazenamentoLocal.cs  # Implementação LocalStorage
│   └── ServicoRenderizacaoMermaid.cs # Integração Mermaid.js
... (e assim por diante)


### Padrões Implementados
* **Template Method:** Utilizado em `ServicoArmazenamentoBase` para definir um esqueleto de algoritmo, permitindo que subclasses (como `ServicoArmazenamentoLocal`) redefinam etapas específicas.
* **Dependency Injection:** Serviços são registrados em `Program.cs` e injetados nos componentes via `@inject`, promovendo baixo acoplamento.
* **Component Pattern:** A UI é construída com componentes que encapsulam sua própria lógica e se comunicam de forma controlada através de parâmetros e callbacks.

### Como Adicionar Novas Funcionalidades
* **Novo Tipo de Armazenamento:** Herde de `ServicoArmazenamentoBase`, implemente os métodos abstratos e registre o novo serviço no container de injeção de dependência.
* **Novo Componente UI:** Crie um novo arquivo `.razor` na pasta `Componentes/`, defina seus `[Parameter]` e integre-o nas páginas existentes.

</details>

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---
**(Fim do Novo README.md)**












































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