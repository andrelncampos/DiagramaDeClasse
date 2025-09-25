# Editor de Diagramas de Classe

[![Status da Build](https://img.shields.io/badge/build-passing-brightgreen)](#)
[![Licença: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Contribuições: Bem-vindas](https://img.shields.io/badge/contributions-welcome-orange.svg)](#como-contribuir)

Uma ferramenta web moderna, de código aberto, construída com Blazor WebAssembly para criar, editar e visualizar diagramas de classe em tempo real usando a sintaxe intuitiva do Mermaid.js.

> 📸 *Screenshot da aplicação em breve*

## ✨ Funcionalidades Principais

* ✅ Edição de código Mermaid em tempo real
* ✅ Visualização instantânea de diagramas
* ✅ Divisor de painel ajustável para otimizar seu espaço de trabalho
* ✅ Salvamento e carregamento do seu trabalho no navegador (LocalStorage)
* ✅ Exportação do diagrama para SVG
* ✅ Interface moderna e responsiva com MudBlazor

## 🚀 Como Executar Localmente

Para executar o projeto na sua máquina, siga os passos abaixo.

**Pré-requisitos:**
* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

**Passos:**
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/DiagramaDeClasse.git
   cd DiagramaDeClasse
   ```
2. Restaure as dependências e execute a aplicação:
   ```bash
   dotnet restore
   dotnet run
   ```
3. Abra seu navegador e acesse `http://localhost:5000` (ou a porta indicada no terminal).

## 🤝 Como Contribuir

Contribuições são extremamente bem-vindas! Este projeto existe graças a pessoas como você. Se você tem uma ideia, uma sugestão ou encontrou um bug, por favor, não hesite em colaborar.

O processo geral é:
1. **Abra uma Issue:** Discuta a mudança que você quer fazer abrindo uma issue.
2. **Faça o Fork:** Crie uma cópia do repositório na sua conta.
3. **Crie uma Branch:** Crie uma branch descritiva para sua alteração.
4. **Envie um Pull Request:** Envie suas alterações para revisão.

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
```
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
├── Pages/                    # Páginas da aplicação
│   └── DiagramEditor.razor         # Página principal
└── Layout/                   # Layout base
    └── MainLayout.razor            # Template da aplicação
```

### Padrões Implementados
* **Template Method:** Utilizado em `ServicoArmazenamentoBase` para definir um esqueleto de algoritmo, permitindo que subclasses redefinam etapas específicas.
* **Dependency Injection:** Serviços são registrados em `Program.cs` e injetados nos componentes via `@inject`, promovendo baixo acoplamento.
* **Component Pattern:** A UI é construída com componentes que encapsulam sua própria lógica e se comunicam de forma controlada através de parâmetros e callbacks.

### Como Adicionar Novas Funcionalidades
* **Novo Tipo de Armazenamento:** Herde de `ServicoArmazenamentoBase`, implemente os métodos abstratos e registre o novo serviço no container de injeção de dependência.
* **Novo Componente UI:** Crie um novo arquivo `.razor` na pasta `Componentes/`, defina seus `[Parameter]` e integre-o nas páginas existentes.
* **Nova Funcionalidade de Diagrama:** Estenda `DiagramaMermaid` se necessário, adicione método em `ServicoRenderizacaoMermaid` e atualize componentes UI relacionados.

</details>

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

*Feito com ❤️ pela comunidade*