using System.Collections.Concurrent;
using PizzariaByte.Mvc.Models;

namespace PizzariaByte.Mvc.Data;

/// <summary>
/// Armazenamento de pedidos em memória.
/// </summary>
/// <remarks>
/// Registrado como Singleton em Program.cs: se fosse Scoped, cada requisição teria
/// um dicionário novo e os pedidos "sumiriam". Mesmo assim, tudo é perdido quando
/// a aplicação é reiniciada. O ConcurrentDictionary protege contra requisições simultâneas.
/// </remarks>
public sealed class PedidoEmMemoriaRepository : IPedidoRepository
{
    private readonly ConcurrentDictionary<Guid, Pedido> _pedidos = new();

    public Task AdicionarAsync(Pedido pedido)
    {
        if (!_pedidos.TryAdd(pedido.Id, pedido))
        {
            throw new InvalidOperationException("Pedido já existe.");
        }

        return Task.CompletedTask;
    }

    public Task AtualizarAsync(Pedido pedido)
    {
        _pedidos[pedido.Id] = pedido;
        return Task.CompletedTask;
    }

    public Task<Pedido?> ObterPorIdAsync(Guid id) =>
        Task.FromResult(_pedidos.TryGetValue(id, out var pedido) ? pedido : null);
}
