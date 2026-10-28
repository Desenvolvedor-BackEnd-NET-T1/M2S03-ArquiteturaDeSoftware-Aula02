using PizzariaByte.Hexagonal.Core.Dominio;
using PizzariaByte.Hexagonal.Core.Portas.Entrada;
using PizzariaByte.Hexagonal.Core.Portas.Saida;

namespace PizzariaByte.Hexagonal.Core.Aplicacao;

/// <summary>
/// Consultas usadas pelas telas. Existe para que as telas não conversem
/// diretamente com os repositórios.
/// </summary>
public sealed class ConsultasPizzariaService : IConsultasPizzariaUseCase
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
