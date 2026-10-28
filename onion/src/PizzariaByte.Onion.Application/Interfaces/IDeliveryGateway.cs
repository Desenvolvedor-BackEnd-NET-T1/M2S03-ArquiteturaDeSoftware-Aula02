using PizzariaByte.Onion.Domain.Entidades;

namespace PizzariaByte.Onion.Application.Interfaces;

/// <summary>
/// Contrato de serviço externo da aplicação (fica na Application porque é uma necessidade do caso de uso): solicitação de entrega (ApiDeliveryAiQFome). Retorna o protocolo.
/// </summary>
public interface IDeliveryGateway
{
    Task<string> SolicitarEntregaAsync(Pedido pedido);
}
