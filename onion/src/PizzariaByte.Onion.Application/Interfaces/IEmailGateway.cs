using PizzariaByte.Onion.Domain.Entidades;

namespace PizzariaByte.Onion.Application.Interfaces;

/// <summary>
/// Contrato de serviço externo da aplicação (fica na Application porque é uma necessidade do caso de uso): envio da confirmação do pedido por e-mail (ApiEmail).
/// </summary>
public interface IEmailGateway
{
    Task EnviarConfirmacaoAsync(Pedido pedido);
}
