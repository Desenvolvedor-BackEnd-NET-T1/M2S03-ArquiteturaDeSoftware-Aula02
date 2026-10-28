using PizzariaByte.Hexagonal.Core.Portas.Saida;

namespace PizzariaByte.Hexagonal.Tests.Fakes;

/// <summary>
/// Adaptador de teste: implementa a mesma porta que o adaptador real, sem nenhum I/O.
/// </summary>
public sealed class FakeObservabilityGateway(bool falhar = false) : IObservabilityGateway
{
    public List<string> Eventos { get; } = [];

    public Task RegistrarEventoAsync(string evento, string detalhe)
    {
        if (falhar)
        {
            throw new InvalidOperationException("Observabilidade fora do ar (teste).");
        }

        Eventos.Add(evento);
        return Task.CompletedTask;
    }
}
