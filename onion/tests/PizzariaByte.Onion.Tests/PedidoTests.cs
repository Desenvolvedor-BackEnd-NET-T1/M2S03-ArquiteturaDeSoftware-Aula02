using PizzariaByte.Onion.Domain.Entidades;
using PizzariaByte.Onion.Domain.Servicos;

namespace PizzariaByte.Onion.Tests;

public sealed class PedidoTests
{
    public static TheoryData<decimal, decimal> TaxasPorSubtotal => new()
    {
        { 45.00m, 8.00m },
        { 99.99m, 8.00m },
        { 100.00m, 0.00m },
        { 165.00m, 0.00m }
    };

    [Theory]
    [MemberData(nameof(TaxasPorSubtotal))]
    public void Calcular_RetornaTaxaConformeSubtotal(decimal subtotal, decimal taxaEsperada)
    {
        // Act
        var taxa = TaxaEntrega.Calcular(subtotal);

        // Assert
        Assert.Equal(taxaEsperada, taxa);
    }

    [Fact]
    public void Criar_ComDadosValidos_DefineIdEStatusInicialRecebido()
    {
        // Arrange
        var pizza = new Pizza(1, "Margherita", "Molho de tomate, muçarela e manjericão", 45.00m);

        // Act
        var pedido = Pedido.Criar(pizza, 1, "Rua das Flores, 123");

        // Assert
        Assert.NotEqual(Guid.Empty, pedido.Id);
        Assert.Equal(StatusPedido.Recebido, pedido.Status);
    }

    [Fact]
    public void Criar_DuasPizzasCalabresa_IsentaTaxa()
    {
        // Arrange
        var pizza = new Pizza(2, "Calabresa", "Molho de tomate, muçarela, calabresa e cebola", 50.00m);

        // Act
        var pedido = Pedido.Criar(pizza, 2, "Rua das Flores, 123");

        // Assert
        Assert.Equal(100m, pedido.Subtotal);
        Assert.Equal(0m, pedido.TaxaEntrega);
        Assert.Equal(100m, pedido.Total);
    }
}
