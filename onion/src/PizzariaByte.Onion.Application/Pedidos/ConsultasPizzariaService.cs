using PizzariaByte.Onion.Domain.Entidades;
using PizzariaByte.Onion.Domain.Repositorios;

namespace PizzariaByte.Onion.Application.Pedidos;

/// <summary>
/// Consultas usadas pelas telas. Existe para que as telas não conversem
/// diretamente com os repositórios.
/// </summary>
/// <remarks>
/// Na Onion, o anel de aplicação é consumido diretamente pela camada externa; a regra é que as
/// dependências apontem para dentro, não que tudo tenha interface.
/// </remarks>
public sealed class ConsultasPizzariaService
{
    private readonly ICardapioRepository _cardapio;
    private readonly IPedidoRepository _pedidos;

    public ConsultasPizzariaService(ICardapioRepository cardapio, IPedidoRepository pedidos)
    {
        _cardapio = cardapio;
        _pedidos = pedidos;
    }

    public Task<IReadOnlyList<Pizza>> ListarCardapioAsync() => _cardapio.ListarAsync();

    public Task<Pizza?> ObterPizzaAsync(int id) => _cardapio.ObterPorIdAsync(id);

    public Task<Pedido?> ObterPedidoAsync(Guid id) => _pedidos.ObterPorIdAsync(id);
}
