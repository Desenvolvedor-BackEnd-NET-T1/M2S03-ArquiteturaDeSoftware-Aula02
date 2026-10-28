using PizzariaByte.Hexagonal.Adapters.Integracoes;
using PizzariaByte.Hexagonal.Adapters.Persistencia;
using PizzariaByte.Hexagonal.Core.Aplicacao;
using PizzariaByte.Hexagonal.Core.Portas.Entrada;
using PizzariaByte.Hexagonal.Core.Portas.Saida;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    // Mensagens de conversão de valores em português (ex.: "abc" no campo Quantidade).
    var mensagens = options.ModelBindingMessageProvider;
    mensagens.SetValueMustBeANumberAccessor(campo => $"O campo {campo} deve ser um número.");
    mensagens.SetAttemptedValueIsInvalidAccessor((valor, campo) => $"O valor '{valor}' não é válido para {campo}.");
});

// ---------------------------------------------------------------------------
// Raiz de composição: é AQUI que cada PORTA é ligada ao seu ADAPTADOR.
// Este projeto Web só referencia os adaptadores de saída para fazer estes registros;
// as páginas conhecem apenas as portas de entrada do núcleo.
//
// Trocando um adaptador (sem tocar no núcleo):
//     builder.Services.AddScoped<IEmailGateway, SmtpEmailAdapter>();
// Os testes fazem o mesmo: passam "adaptadores de teste" (fakes) para as mesmas portas.
// ---------------------------------------------------------------------------
var falhaEmail = builder.Configuration.GetValue<bool>("Integracoes:ApiEmail:SimularFalha");
var falhaDelivery = builder.Configuration.GetValue<bool>("Integracoes:ApiDeliveryAiQFome:SimularFalha");

// Portas de saída → adaptadores de persistência (Singleton: os dados vivem enquanto a aplicação roda).
builder.Services.AddSingleton<ICardapioRepository, CardapioEmMemoriaAdapter>();
builder.Services.AddSingleton<IPedidoRepository, PedidoEmMemoriaAdapter>();

// Portas de saída → adaptadores de integração (simulados).
builder.Services.AddScoped<IEmailGateway>(_ => new ApiEmail(new ConfiguracaoMock(falhaEmail)));
builder.Services.AddScoped<IDeliveryGateway>(_ => new ApiDeliveryAiQFome(new ConfiguracaoMock(falhaDelivery)));
builder.Services.AddScoped<IObservabilityGateway, ApiMObservabilidade>();

// Portas de entrada → implementações do núcleo (casos de uso).
builder.Services.AddScoped<ICriarPedidoUseCase, CriarPedidoService>();
builder.Services.AddScoped<IConsultasPizzariaUseCase, ConsultasPizzariaService>();

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
