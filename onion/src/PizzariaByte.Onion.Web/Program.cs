using PizzariaByte.Onion.Application.Interfaces;
using PizzariaByte.Onion.Application.Pedidos;
using PizzariaByte.Onion.Domain.Repositorios;
using PizzariaByte.Onion.Infrastructure.Integracoes;
using PizzariaByte.Onion.Infrastructure.Persistencia;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    // Mensagens de conversão de valores em português (ex.: "abc" no campo Quantidade).
    var mensagens = options.ModelBindingMessageProvider;
    mensagens.SetValueMustBeANumberAccessor(campo => $"O campo {campo} deve ser um número.");
    mensagens.SetAttemptedValueIsInvalidAccessor((valor, campo) => $"O valor '{valor}' não é válido para {campo}.");
});

// ---------------------------------------------------------------------------
// Composição da DI no anel mais externo (Web).
// A Web conhece a Infrastructure apenas para registrar implementações;
// Domain e Application nunca conhecem a Infrastructure — as dependências apontam para dentro.
//
// Contratos do Domain (repositórios) e da Application (serviços externos)
// são ligados às implementações da Infrastructure. Para trocar uma implementação:
//     builder.Services.AddScoped<IEmailGateway, SmtpEmailGateway>();
// ---------------------------------------------------------------------------
var falhaEmail = builder.Configuration.GetValue<bool>("Integracoes:ApiEmail:SimularFalha");
var falhaDelivery = builder.Configuration.GetValue<bool>("Integracoes:ApiDeliveryAiQFome:SimularFalha");

// Repositórios em memória são Singleton: os dados vivem enquanto a aplicação roda.
builder.Services.AddSingleton<ICardapioRepository, CardapioEmMemoriaRepository>();
builder.Services.AddSingleton<IPedidoRepository, PedidoEmMemoriaRepository>();

builder.Services.AddScoped<IEmailGateway>(_ => new ApiEmail(new ConfiguracaoMock(falhaEmail)));
builder.Services.AddScoped<IDeliveryGateway>(_ => new ApiDeliveryAiQFome(new ConfiguracaoMock(falhaDelivery)));
builder.Services.AddScoped<IObservabilityGateway, ApiMObservabilidade>();

// Serviços de aplicação: classes concretas, consumidas diretamente pela camada Web.
builder.Services.AddScoped<CriarPedidoService>();
builder.Services.AddScoped<ConsultasPizzariaService>();

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

app.MapRazorPages();

app.Run();
