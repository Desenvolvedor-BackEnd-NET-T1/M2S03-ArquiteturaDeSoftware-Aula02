using PizzariaByte.Hexagonal.Core.Dominio;

namespace PizzariaByte.Hexagonal.Core.Portas.Saida;

/// <summary>
/// Porta de saída: persistência de pedidos.
/// </summary>
public interface IPedidoRepository
{
    Task AdicionarAsync(Pedido pedido);

    Task AtualizarAsync(Pedido pedido);

    Task<Pedido?> ObterPorIdAsync(Guid id);
}
