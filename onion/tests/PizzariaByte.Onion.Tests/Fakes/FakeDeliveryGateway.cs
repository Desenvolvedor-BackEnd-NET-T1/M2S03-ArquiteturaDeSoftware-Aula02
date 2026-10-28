using PizzariaByte.Onion.Application.Interfaces;
using PizzariaByte.Onion.Domain.Entidades;

namespace PizzariaByte.Onion.Tests.Fakes;

/// <summary>
/// Implementação falsa para testes: substitui a real sem nenhum I/O.
/// </summary>
public sealed class FakeDeliveryGateway(bool falhar = false) : IDeliveryGateway
{
    public int Chamadas { get; private set; }

    public Task<string> SolicitarEntregaAsync(Pedido pedido)
    {
        Chamadas++;
        if (falhar)
        {
            throw new IntegracaoException("ApiDeliveryAiQFome", "Falha de teste.");
        }

        return Task.FromResult("FAKE-123");
    }
}
