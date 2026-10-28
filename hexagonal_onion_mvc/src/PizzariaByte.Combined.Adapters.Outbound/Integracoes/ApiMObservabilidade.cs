using PizzariaByte.Combined.Application.Ports.Saida;

namespace PizzariaByte.Combined.Adapters.Outbound.Integracoes;

/// <summary>
/// Simulação: escreve os eventos no console em vez de enviá-los a uma plataforma externa.
/// Para usar uma plataforma real, crie outra classe que implemente
/// <see cref="IObservabilityGateway"/> e troque o registro em Program.cs.
/// </summary>
public sealed class ApiMObservabilidade : IObservabilityGateway
{
    public Task RegistrarEventoAsync(string evento, string detalhe)
    {
        Console.WriteLine($"[ApiMObservabilidade] {DateTime.UtcNow:O} {evento}: {detalhe}");
        return Task.CompletedTask;
    }
}
