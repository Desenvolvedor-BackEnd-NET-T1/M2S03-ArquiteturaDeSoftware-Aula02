using PizzariaByte.Onion.Domain.Entidades;

namespace PizzariaByte.Onion.Domain.Repositorios;

/// <summary>
/// Contrato de repositório do domínio (fica no centro porque descreve coleções de entidades): persistência de pedidos.
/// </summary>
public interface IPedidoRepository
{
    Task AdicionarAsync(Pedido pedido);

    Task AtualizarAsync(Pedido pedido);

    Task<Pedido?> ObterPorIdAsync(Guid id);
}
