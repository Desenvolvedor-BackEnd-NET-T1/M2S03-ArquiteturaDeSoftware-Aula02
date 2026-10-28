using PizzariaByte.Combined.Application.Ports.Saida;
using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Tests.Fakes;

/// <summary>
/// Adaptador de teste: implementa a mesma porta que o adaptador real, sem nenhum I/O.
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
