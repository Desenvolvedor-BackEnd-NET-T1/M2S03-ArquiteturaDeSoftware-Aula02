namespace PizzariaByte.Combined.Application.Ports.Saida;

/// <summary>
/// Porta de saída (Hexagonal), definida no anel de aplicação (Onion): registro de eventos de observabilidade (ApiMObservabilidade).
/// </summary>
public interface IObservabilityGateway
{
    Task RegistrarEventoAsync(string evento, string detalhe);
}
