using PizzariaByte.Hexagonal.Core.Dominio;

namespace PizzariaByte.Hexagonal.Core.Portas.Saida;

/// <summary>
/// Porta de saída: envio da confirmação do pedido por e-mail (ApiEmail).
/// </summary>
public interface IEmailGateway
{
    Task EnviarConfirmacaoAsync(Pedido pedido);
}
