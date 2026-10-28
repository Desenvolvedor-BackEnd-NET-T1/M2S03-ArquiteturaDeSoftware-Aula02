using PizzariaByte.Mvc.Integrations;

namespace PizzariaByte.Mvc.Tests.Fakes;

/// <summary>
/// Implementação falsa para testes: substitui a real sem nenhum I/O.
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
