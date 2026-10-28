using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Application.Ports.Saida;

/// <summary>
/// Porta de saída (Hexagonal), definida no anel de aplicação (Onion): envio da confirmação do pedido por e-mail (ApiEmail).
/// </summary>
public interface IEmailGateway
{
    Task EnviarConfirmacaoAsync(Pedido pedido);
}
