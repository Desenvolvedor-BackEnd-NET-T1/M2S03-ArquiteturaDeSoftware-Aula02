using PizzariaByte.Combined.Application.Ports.Saida;
using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Tests.Fakes;

/// <summary>
/// Adaptador de teste: implementa a mesma porta que o adaptador real, sem nenhum I/O.
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
