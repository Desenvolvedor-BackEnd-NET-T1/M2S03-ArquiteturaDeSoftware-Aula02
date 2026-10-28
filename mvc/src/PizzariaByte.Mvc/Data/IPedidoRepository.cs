using PizzariaByte.Mvc.Models;

namespace PizzariaByte.Mvc.Data;

/// <summary>
/// Abstração de persistência de pedidos.
/// </summary>
public interface IPedidoRepository
{
    Task AdicionarAsync(Pedido pedido);

    Task AtualizarAsync(Pedido pedido);

    Task<Pedido?> ObterPorIdAsync(Guid id);
}
