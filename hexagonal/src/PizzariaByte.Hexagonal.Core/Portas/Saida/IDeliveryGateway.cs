using PizzariaByte.Hexagonal.Core.Dominio;

namespace PizzariaByte.Hexagonal.Core.Portas.Saida;

/// <summary>
/// Porta de saída: solicitação de entrega (ApiDeliveryAiQFome). Retorna o protocolo.
/// </summary>
public interface IDeliveryGateway
{
    Task<string> SolicitarEntregaAsync(Pedido pedido);
}
