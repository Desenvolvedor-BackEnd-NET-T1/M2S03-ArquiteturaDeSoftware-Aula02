using PizzariaByte.Combined.Application.Ports.Saida;
using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Tests.Fakes;

/// <summary>
/// Adaptador de teste: implementa a mesma porta que o adaptador real, sem nenhum I/O.
/// </summary>
public sealed class FakeEmailGateway(bool falhar = false) : IEmailGateway
{
    public int Chamadas { get; private set; }

    public Task EnviarConfirmacaoAsync(Pedido pedido)
    {
        Chamadas++;
        if (falhar)
        {
            throw new IntegracaoException("ApiEmail", "Falha de teste.");
        }

        return Task.CompletedTask;
    }
}
