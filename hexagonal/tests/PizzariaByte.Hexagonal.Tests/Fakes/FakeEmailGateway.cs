using PizzariaByte.Hexagonal.Core.Portas.Saida;
using PizzariaByte.Hexagonal.Core.Dominio;

namespace PizzariaByte.Hexagonal.Tests.Fakes;

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
