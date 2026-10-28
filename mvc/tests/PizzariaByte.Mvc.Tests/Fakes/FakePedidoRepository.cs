using PizzariaByte.Mvc.Data;
using PizzariaByte.Mvc.Models;

namespace PizzariaByte.Mvc.Tests.Fakes;

/// <summary>
/// Implementação falsa para testes: substitui a real sem nenhum I/O.
/// </summary>
public sealed class FakePedidoRepository : IPedidoRepository
{
    public List<Pedido> Pedidos { get; } = [];

    public int Atualizacoes { get; private set; }

    public Task AdicionarAsync(Pedido pedido)
    {
        Pedidos.Add(pedido);
        return Task.CompletedTask;
    }

    public Task AtualizarAsync(Pedido pedido)
    {
        Atualizacoes++;
        return Task.CompletedTask;
    }

    public Task<Pedido?> ObterPorIdAsync(Guid id) => Task.FromResult(Pedidos.FirstOrDefault(p => p.Id == id));
}
