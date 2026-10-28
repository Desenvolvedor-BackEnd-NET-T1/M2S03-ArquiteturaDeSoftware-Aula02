using PizzariaByte.Onion.Application.Interfaces;
using PizzariaByte.Onion.Domain.Entidades;

namespace PizzariaByte.Onion.Tests.Fakes;

/// <summary>
/// Implementação falsa para testes: substitui a real sem nenhum I/O.
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
