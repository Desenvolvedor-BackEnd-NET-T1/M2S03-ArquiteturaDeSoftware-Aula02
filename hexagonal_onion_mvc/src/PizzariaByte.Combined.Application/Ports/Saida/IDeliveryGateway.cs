using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Application.Ports.Saida;

/// <summary>
/// Porta de saída (Hexagonal), definida no anel de aplicação (Onion): solicitação de entrega (ApiDeliveryAiQFome). Retorna o protocolo.
/// </summary>
public interface IDeliveryGateway
{
    Task<string> SolicitarEntregaAsync(Pedido pedido);
}
