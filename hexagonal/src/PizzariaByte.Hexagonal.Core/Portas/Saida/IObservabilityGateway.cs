namespace PizzariaByte.Hexagonal.Core.Portas.Saida;

/// <summary>
/// Porta de saída: registro de eventos de observabilidade (ApiMObservabilidade).
/// </summary>
public interface IObservabilityGateway
{
    Task RegistrarEventoAsync(string evento, string detalhe);
}
