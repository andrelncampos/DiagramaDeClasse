using DiagramaDeClasse;
using DiagramaDeClasse.Servicos;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpClient>();

// Configuração completa do MudBlazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomLeft;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = MudBlazor.Variant.Filled;
});

// Registrando serviços da aplicação
builder.Services.AddScoped<ServicoArmazenamentoBase, ServicoArmazenamentoLocal>();
builder.Services.AddScoped<ServicoRenderizacaoMermaid>();
builder.Services.AddScoped<ServicoCriptografia>();
builder.Services.AddScoped<ServicoConfiguracaoIA>();
builder.Services.AddScoped<ServicoIA>();

await builder.Build().RunAsync();
