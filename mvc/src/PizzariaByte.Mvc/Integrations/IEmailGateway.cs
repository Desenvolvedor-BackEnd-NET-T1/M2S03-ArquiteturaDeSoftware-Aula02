using PizzariaByte.Mvc.Models;

namespace PizzariaByte.Mvc.Integrations;

/// <summary>
/// Abstração do serviço de e-mail. Implementação atual: <see cref="ApiEmail"/> (simulada).
/// </summary>
public interface IEmailGateway
{
    Task EnviarConfirmacaoAsync(Pedido pedido);
}
