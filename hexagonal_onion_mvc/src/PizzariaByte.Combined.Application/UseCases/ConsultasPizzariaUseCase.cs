using PizzariaByte.Combined.Domain;
using PizzariaByte.Combined.Application.Ports.Entrada;
using PizzariaByte.Combined.Application.Ports.Saida;

namespace PizzariaByte.Combined.Application.UseCases;

/// <summary>
/// Consultas usadas pelas telas. Existe para que as telas não conversem
/// diretamente com os repositórios.
/// </summary>
public sealed class ConsultasPizzariaUseCase : IConsultasPizzariaUseCase
{
    private readonly ICardapioRepository _cardapio;
    private readonly IPedidoRepository _pedidos;

    public ConsultasPizzariaUseCase(ICardapioRepository cardapio, IPedidoRepository pedidos)
    {
        _cardapio = cardapio;
        _pedidos = pedidos;
    }

    public Task<IReadOnlyList<Pizza>> ListarCardapioAsync() => _cardapio.ListarAsync();

    public Task<Pizza?> ObterPizzaAsync(int id) => _cardapio.ObterPorIdAsync(id);

    public Task<Pedido?> ObterPedidoAsync(Guid id) => _pedidos.ObterPorIdAsync(id);
}
