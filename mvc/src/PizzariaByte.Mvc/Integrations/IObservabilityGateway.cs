namespace PizzariaByte.Mvc.Integrations;

/// <summary>
/// Abstração do registro de eventos de observabilidade.
/// Implementação atual: <see cref="ApiMObservabilidade"/> (simulada).
/// </summary>
public interface IObservabilityGateway
{
    Task RegistrarEventoAsync(string evento, string detalhe);
}
