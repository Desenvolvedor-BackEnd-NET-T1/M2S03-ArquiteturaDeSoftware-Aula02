using PizzariaByte.Combined.Adapters.Outbound.Integracoes;
using PizzariaByte.Combined.Adapters.Outbound.Persistencia;
using PizzariaByte.Combined.Application.Ports.Entrada;
using PizzariaByte.Combined.Application.Ports.Saida;
using PizzariaByte.Combined.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// MVC: a interface web é organizada em Controllers, Models (de apresentação) e Views.
builder.Services.AddControllersWithViews(options =>
{
    // Mensagens de conversão de valores em português (ex.: "abc" no campo Quantidade).
    var mensagens = options.ModelBindingMessageProvider;
    mensagens.SetValueMustBeANumberAccessor(campo => $"O campo {campo} deve ser um número.");
    mensagens.SetAttemptedValueIsInvalidAccessor((valor, campo) => $"O valor '{valor}' não é válido para {campo}.");
});

// ---------------------------------------------------------------------------
// Onion: dependências para dentro. Este projeto (anel externo) é o único que conhece
// Application e Adapters.Outbound ao mesmo tempo — e só para ligar uma ao outro aqui.
//
// Hexagonal: porta → adaptador. Para trocar um adaptador sem tocar na Application:
//     builder.Services.AddScoped<IEmailGateway, SmtpEmailAdapter>();
// ---------------------------------------------------------------------------
var falhaEmail = builder.Configuration.GetValue<bool>("Integracoes:ApiEmail:SimularFalha");
var falhaDelivery = builder.Configuration.GetValue<bool>("Integracoes:ApiDeliveryAiQFome:SimularFalha");

// Hexagonal: portas de saída → adaptadores de persistência (Singleton: dados vivem enquanto a aplicação roda).
builder.Services.AddSingleton<ICardapioRepository, CardapioEmMemoriaAdapter>();
builder.Services.AddSingleton<IPedidoRepository, PedidoEmMemoriaAdapter>();

// Hexagonal: portas de saída → adaptadores de integração (simulados).
builder.Services.AddScoped<IEmailGateway>(_ => new ApiEmail(new ConfiguracaoMock(falhaEmail)));
builder.Services.AddScoped<IDeliveryGateway>(_ => new ApiDeliveryAiQFome(new ConfiguracaoMock(falhaDelivery)));
builder.Services.AddScoped<IObservabilityGateway, ApiMObservabilidade>();

// Hexagonal: portas de entrada → casos de uso da Application (Onion).
builder.Services.AddScoped<ICriarPedidoUseCase, CriarPedidoUseCase>();
builder.Services.AddScoped<IConsultasPizzariaUseCase, ConsultasPizzariaUseCase>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // Tratamento global: erros inesperados mostram uma página amigável, sem detalhes internos.
    app.UseExceptionHandler("/Erro");
}

// Cultura fixa pt-BR: "R$ 98,00" em Windows, Linux e macOS, independentemente do sistema.
app.UseRequestLocalization("pt-BR");
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
