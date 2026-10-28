namespace PizzariaByte.Onion.Domain.Entidades;

/// <summary>
/// Ciclo de vida mínimo do pedido.
/// </summary>
public enum StatusPedido
{
    /// <summary>Status inicial: o pedido foi validado, criado e persistido.</summary>
    Recebido,

    /// <summary>A entrega foi solicitada com sucesso à ApiDeliveryAiQFome.</summary>
    Confirmado,

    /// <summary>O pedido está salvo, mas a solicitação de entrega falhou.</summary>
    EntregaPendente
}
