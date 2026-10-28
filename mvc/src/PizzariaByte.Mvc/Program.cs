using PizzariaByte.Mvc.Data;
using PizzariaByte.Mvc.Integrations;
using PizzariaByte.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    // Mensagens de conversão de valores em português (ex.: "abc" no campo Quantidade).
    var mensagens = options.ModelBindingMessageProvider;
    mensagens.SetValueMustBeANumberAccessor(campo => $"O campo {campo} deve ser um número.");
    mensagens.SetAttemptedValueIsInvalidAccessor((valor, campo) => $"O valor '{valor}' não é válido para {campo}.");
});

// ---------------------------------------------------------------------------
// Injeção de dependência: aqui cada abstração é ligada à sua implementação.
//
// Repositórios em memória são Singleton: uma única instância durante toda a execução.
// Se fossem Scoped, cada requisição teria uma lista vazia e os pedidos "sumiriam".
//
// Para TROCAR uma implementação, basta mudar o registro. Exemplo:
//     builder.Services.AddScoped<IEmailGateway, SmtpEmailGateway>();
// O CriarPedidoService não muda, porque depende apenas de IEmailGateway.
// ---------------------------------------------------------------------------
var falhaEmail = builder.Configuration.GetValue<bool>("Integracoes:ApiEmail:SimularFalha");
var falhaDelivery = builder.Configuration.GetValue<bool>("Integracoes:ApiDeliveryAiQFome:SimularFalha");

builder.Services.AddSingleton<ICardapioRepository, CardapioEmMemoriaRepository>();
builder.Services.AddSingleton<IPedidoRepository, PedidoEmMemoriaRepository>();

builder.Services.AddScoped<IEmailGateway>(_ => new ApiEmail(new ConfiguracaoMock(falhaEmail)));
builder.Services.AddScoped<IDeliveryGateway>(_ => new ApiDeliveryAiQFome(new ConfiguracaoMock(falhaDelivery)));
builder.Services.AddScoped<IObservabilityGateway, ApiMObservabilidade>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
