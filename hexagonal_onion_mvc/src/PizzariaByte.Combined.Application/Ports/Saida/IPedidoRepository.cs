using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Application.Ports.Saida;

/// <summary>
/// Porta de saída (Hexagonal), definida no anel de aplicação (Onion): persistência de pedidos.
/// </summary>
public interface IPedidoRepository
{
    Task AdicionarAsync(Pedido pedido);

    Task AtualizarAsync(Pedido pedido);

    Task<Pedido?> ObterPorIdAsync(Guid id);
}
