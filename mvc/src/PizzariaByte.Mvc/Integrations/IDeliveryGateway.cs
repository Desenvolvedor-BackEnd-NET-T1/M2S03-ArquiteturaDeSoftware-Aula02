using PizzariaByte.Mvc.Models;

namespace PizzariaByte.Mvc.Integrations;

/// <summary>
/// Abstração do serviço de entrega. Retorna o protocolo da solicitação.
/// Implementação atual: <see cref="ApiDeliveryAiQFome"/> (simulada).
/// </summary>
public interface IDeliveryGateway
{
    Task<string> SolicitarEntregaAsync(Pedido pedido);
}
