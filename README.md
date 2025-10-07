# Diagramas Expert

[![Status da Build](https://img.shields.io/badge/build-passing-brightgreen)](#)
[![Licença: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Contribuições: Bem-vindas](https://img.shields.io/badge/contributions-welcome-orange.svg)](#como-contribuir)

## 🌐 Acesse a Aplicação

**[https://andrelncampos.github.io/DiagramaDeClasse/](https://andrelncampos.github.io/DiagramaDeClasse/)**

Uma ferramenta web moderna, de código aberto, construída com Blazor WebAssembly para criar, editar e visualizar 11 tipos diferentes de diagramas em tempo real usando a sintaxe intuitiva do Mermaid.js, com suporte a IA para geração automática.

## ✨ Funcionalidades Principais

### 📊 Tipos de Diagramas Suportados
* Diagrama de Classe
* Fluxograma
* Diagrama de Sequência
* Diagrama de Estado
* Diagrama ER (Entidade-Relacionamento)
* Gráfico de Gantt
* Gráfico de Pizza
* Jornada do Usuário
* Diagrama Git
* Mindmap
* Timeline

### 🎨 Editor e Visualização
* ✅ Edição de código Mermaid em tempo real com debounce
* ✅ Visualização instantânea de diagramas
* ✅ Divisor de painel ajustável para otimizar seu espaço de trabalho
* ✅ Controles de zoom (in, out, reset)
* ✅ Detecção automática de tipo ao abrir arquivo
* ✅ Interface moderna e responsiva com MudBlazor

### 🤖 Integração com IA
* ✅ Geração automática de diagramas via prompt
* ✅ Suporte a múltiplos provedores: Google Gemini, OpenAI, Claude
* ✅ Prompt contextual por tipo de diagrama
* ✅ Configuração criptografada (AES-GCM 256-bit)
* ✅ Guia para obter API gratuita do Google Gemini

### 💾 Armazenamento e Exportação
* ✅ Salvamento automático no LocalStorage (por tipo de diagrama)
* ✅ Exportação para arquivo .dcl (disco)
* ✅ Exportação para SVG
* ✅ Exportação para PDF
* ✅ Abertura de arquivos com detecção automática de tipo

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

* **Blazor WebAssembly (.NET 9.0)**: Framework UI principal
* **MudBlazor**: Biblioteca de componentes Material Design
* **Mermaid.js 11.4.0**: Biblioteca para renderização de diagramas
* **Web Crypto API**: Criptografia AES-GCM para credenciais
* **LocalStorage**: Persistência local de dados
* **jsPDF**: Exportação para PDF
* **File System Access API**: Salvamento/abertura de arquivos

## 🎯 Como Usar a IA

1. **Configure a IA**: Clique em "Configurar IA" na toolbar
2. **Obtenha chave gratuita**: Clique em "Obtenha IA Gratuita" para instruções do Google Gemini
3. **Selecione o tipo**: Escolha o tipo de diagrama no dropdown
4. **Use o Prompt**: Vá na aba "Prompt IA" e digite sua solicitação
5. **Gere**: Clique em "Gerar Diagrama" e veja a mágica acontecer!

### Exemplos de Prompts
* "Crie um diagrama de classe para um sistema de biblioteca"
* "Adicione uma classe Autor com nome e nacionalidade"
* "Crie um fluxograma de processo de compra online"
* "Gere um cronograma de projeto de 3 meses"

## 🗺️ Roadmap de Melhorias

* ✅ Exportação para PNG e PDF
* ✅ Integração com IA (Google Gemini, OpenAI, Claude)
* ✅ Detecção automática de tipo de diagrama
* 🔄 Múltiplos diagramas em abas
* 🔄 Armazenamento em nuvem (GitHub Gist)
* 🔄 Colaboração em tempo real
* 🔄 Histórico de versões (undo/redo)
* 🔄 Temas personalizados

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
│   ├── BarraFerramentas.razor           # Toolbar com ações
│   ├── EditorCodigo.razor               # Editor de texto Mermaid
│   ├── VisualizadorDiagrama.razor       # Renderização do diagrama
│   ├── PromptIA.razor                   # Interface de prompt IA
│   ├── DialogoConfiguracaoIA.razor      # Configuração de IA
│   ├── DialogoIAGratuita.razor          # Guia de IA gratuita
│   ├── DialogoHelp.razor                # Ajuda por tipo
│   └── DialogoSobre.razor               # Sobre a aplicação
├── Modelos/                  # Entidades de domínio
│   ├── DiagramaMermaid.cs               # Modelo do diagrama
│   ├── TipoDiagrama.cs                  # Tipos e exemplos
│   └── ConfiguracaoIA.cs                # Configuração de IA
├── Servicos/                 # Lógica de negócio
│   ├── ServicoArmazenamentoBase.cs      # Classe abstrata base
│   ├── ServicoArmazenamentoLocal.cs     # LocalStorage
│   ├── ServicoRenderizacaoMermaid.cs    # Integração Mermaid.js
│   ├── ServicoCriptografia.cs           # Criptografia AES-GCM
│   ├── ServicoConfiguracaoIA.cs         # Gestão de config IA
│   └── ServicoIA.cs                     # Integração APIs IA
├── Pages/                    # Páginas da aplicação
│   └── DiagramEditor.razor              # Página principal
├── Layout/                   # Layout base
│   └── MainLayout.razor                 # Template da aplicação
└── wwwroot/js/               # JavaScript Interop
    ├── mermaidInterop.js                # Mermaid + File API
    └── cryptoInterop.js                 # Web Crypto API
```

### Padrões Implementados
* **Template Method:** `ServicoArmazenamentoBase` define esqueleto de algoritmo
* **Dependency Injection:** Serviços registrados em `Program.cs` e injetados via `@inject`
* **Component Pattern:** UI componentizada com baixo acoplamento
* **Strategy Pattern:** Múltiplos provedores de IA (OpenAI, Google, Claude)
* **Observer Pattern:** EventCallback para comunicação entre componentes

### Segurança
* **Criptografia AES-GCM 256-bit** para credenciais de IA
* **PBKDF2** com 100.000 iterações para derivação de chave
* **Web Crypto API** nativa do navegador
* **Armazenamento local** - dados nunca saem do navegador
* **Sem backend** - aplicação 100% client-side

### Como Adicionar Novas Funcionalidades
* **Novo Tipo de Diagrama:** Adicione em `TipoDiagrama.ObterTodos()` com exemplo
* **Novo Provedor de IA:** Adicione em `ProvedorIA` enum e implemente método em `ServicoIA`
* **Novo Tipo de Armazenamento:** Herde de `ServicoArmazenamentoBase` e registre em `Program.cs`
* **Novo Componente UI:** Crie `.razor` em `Componentes/` e integre nas páginas

### Layout Responsivo
* **CSS Grid** para layout principal sem scroll
* **Flexbox** para componentes internos
* **min-height: 0** para permitir overflow correto
* **100vh** para ocupar toda viewport
* **Painel ajustável** com drag & drop

</details>

## 🔒 Privacidade e Segurança

* ✅ **100% Client-Side**: Nenhum dado é enviado para servidores
* ✅ **Criptografia Local**: Credenciais criptografadas com AES-GCM
* ✅ **Sem Cookies**: Usa apenas LocalStorage
* ✅ **Código Aberto**: Audite o código você mesmo
* ✅ **APIs Diretas**: Chama APIs de IA diretamente do navegador

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 🙏 Agradecimentos

* [Mermaid.js](https://mermaid.js.org/) - Biblioteca incrível de diagramas
* [MudBlazor](https://mudblazor.com/) - Componentes Material Design
* [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) - Framework .NET

---

*Feito com ❤️ e IA pela comunidade*