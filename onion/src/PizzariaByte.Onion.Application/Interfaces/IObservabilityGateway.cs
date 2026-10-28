namespace PizzariaByte.Onion.Application.Interfaces;

/// <summary>
/// Contrato de serviço externo da aplicação (fica na Application porque é uma necessidade do caso de uso): registro de eventos de observabilidade (ApiMObservabilidade).
/// </summary>
public interface IObservabilityGateway
{
    Task RegistrarEventoAsync(string evento, string detalhe);
}
