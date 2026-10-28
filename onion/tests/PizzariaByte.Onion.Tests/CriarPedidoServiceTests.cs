using PizzariaByte.Onion.Domain.Entidades;
using PizzariaByte.Onion.Domain.Servicos;
using PizzariaByte.Onion.Application.Pedidos;
using PizzariaByte.Onion.Tests.Fakes;

namespace PizzariaByte.Onion.Tests;

/// <summary>
/// Testes do caso de uso. Cada teste cria seus próprios fakes: nenhum estado é compartilhado,
/// por isso os testes são independentes e podem rodar em qualquer ordem.
/// </summary>
public sealed class CriarPedidoServiceTests
{
    private const string Endereco = "Rua das Flores, 123";

    private static (CriarPedidoService Sut, FakePedidoRepository Repositorio, FakeEmailGateway Email,
        FakeDeliveryGateway Delivery, FakeObservabilityGateway Observabilidade) CriarSut(
        bool falharEmail = false, bool falharDelivery = false, bool falharObservabilidade = false)
    {
        var repositorio = new FakePedidoRepository();
        var email = new FakeEmailGateway(falharEmail);
        var delivery = new FakeDeliveryGateway(falharDelivery);
        var observabilidade = new FakeObservabilityGateway(falharObservabilidade);
        var sut = new CriarPedidoService(new FakeCardapioRepository(), repositorio, email, delivery, observabilidade);
        return (sut, repositorio, email, delivery, observabilidade);
    }

    [Fact]
    public async Task CriarAsync_ComPizzaEQuantidadeValidas_CalculaTotalEPersistePedido()
    {
        // Arrange
        var (sut, repositorio, _, _, _) = CriarSut();

        // Act
        var resultado = await sut.CriarAsync(pizzaId: 1, quantidade: 2, Endereco);

        // Assert
        Assert.True(resultado.Sucesso);
        var pedido = Assert.IsType<Pedido>(resultado.Pedido);
        Assert.Equal(90m, pedido.Subtotal);
        Assert.Equal(8m, pedido.TaxaEntrega);
        Assert.Equal(98m, pedido.Total);
        Assert.Equal(StatusPedido.Confirmado, pedido.Status);
        Assert.Single(repositorio.Pedidos);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CriarAsync_ComQuantidadeZeroOuNegativa_RejeitaENaoPersiste(int quantidade)
    {
        // Arrange
        var (sut, repositorio, email, delivery, _) = CriarSut();

        // Act
        var resultado = await sut.CriarAsync(1, quantidade, Endereco);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.Contains("A quantidade deve ser maior que zero.", resultado.Erros);
        Assert.Empty(repositorio.Pedidos);
        Assert.Equal(0, email.Chamadas);
        Assert.Equal(0, delivery.Chamadas);
    }

    [Fact]
    public async Task CriarAsync_ComPizzaInexistente_RejeitaENaoPersiste()
    {
        // Arrange
        var (sut, repositorio, _, _, _) = CriarSut();

        // Act
        var resultado = await sut.CriarAsync(99, 1, Endereco);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.Contains("A pizza selecionada não existe no cardápio.", resultado.Erros);
        Assert.Empty(repositorio.Pedidos);
    }

    [Fact]
    public async Task CriarAsync_ComEnderecoEmBranco_RejeitaENaoPersiste()
    {
        // Arrange
        var (sut, repositorio, _, _, _) = CriarSut();

        // Act
        var resultado = await sut.CriarAsync(1, 1, "   ");

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.Contains("Informe o endereço de entrega.", resultado.Erros);
        Assert.Empty(repositorio.Pedidos);
    }

    [Fact]
    public async Task CriarAsync_ComQuantidadeAcimaDoLimite_RejeitaENaoPersiste()
    {
        // Arrange
        var (sut, repositorio, _, _, _) = CriarSut();

        // Act
        var resultado = await sut.CriarAsync(1, 51, Endereco);

        // Assert
        Assert.False(resultado.Sucesso);
        Assert.Contains("A quantidade máxima por pedido é 50.", resultado.Erros);
        Assert.Empty(repositorio.Pedidos);
    }

    [Fact]
    public async Task CriarAsync_QuandoEmailFalha_PedidoContinuaConfirmadoEFalhaEhRegistrada()
    {
        // Arrange
        var (sut, repositorio, _, delivery, observabilidade) = CriarSut(falharEmail: true);

        // Act
        var resultado = await sut.CriarAsync(1, 2, Endereco);

        // Assert
        var pedido = Assert.IsType<Pedido>(resultado.Pedido);
        Assert.Equal(StatusPedido.Confirmado, pedido.Status);
        Assert.False(pedido.Integracoes.Single(i => i.Conector == "ApiEmail").Sucesso);
        Assert.Single(repositorio.Pedidos);
        Assert.Equal(1, delivery.Chamadas);
        Assert.Contains("FalhaIntegracao", observabilidade.Eventos);
    }

    [Fact]
    public async Task CriarAsync_QuandoEntregaFalha_PedidoFicaComEntregaPendente()
    {
        // Arrange
        var (sut, repositorio, _, _, _) = CriarSut(falharDelivery: true);

        // Act
        var resultado = await sut.CriarAsync(1, 2, Endereco);

        // Assert
        var pedido = Assert.IsType<Pedido>(resultado.Pedido);
        Assert.Equal(98m, pedido.Total);
        Assert.Equal(StatusPedido.EntregaPendente, pedido.Status);
        Assert.False(pedido.Integracoes.Single(i => i.Conector == "ApiDeliveryAiQFome").Sucesso);
        Assert.Single(repositorio.Pedidos);
    }

    [Fact]
    public async Task CriarAsync_QuandoObservabilidadeFalha_PedidoEhCriadoNormalmente()
    {
        // Arrange
        var (sut, repositorio, _, _, _) = CriarSut(falharObservabilidade: true);

        // Act
        var resultado = await sut.CriarAsync(1, 2, Endereco);

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.Equal(StatusPedido.Confirmado, resultado.Pedido!.Status);
        Assert.Single(repositorio.Pedidos);
    }

    [Fact]
    public async Task CriarAsync_PedidoValido_UtilizaAsAbstracoesInjetadas()
    {
        // Arrange
        var (sut, repositorio, email, delivery, observabilidade) = CriarSut();

        // Act
        await sut.CriarAsync(3, 1, Endereco);

        // Assert
        Assert.Equal(1, email.Chamadas);
        Assert.Equal(1, delivery.Chamadas);
        Assert.Equal(1, repositorio.Atualizacoes);
        Assert.Equal(["PedidoCriado", "PedidoPersistido", "PedidoFinalizado"], observabilidade.Eventos);
    }
}
