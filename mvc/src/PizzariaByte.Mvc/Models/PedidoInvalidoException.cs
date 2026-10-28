namespace PizzariaByte.Mvc.Models;

/// <summary>
/// Violação de uma regra de negócio do pedido. Lançada apenas por <see cref="Pedido.Criar"/>
/// e convertida em <see cref="ResultadoCriacaoPedido"/> pelo serviço de criação.
/// </summary>
public sealed class PedidoInvalidoException : Exception
{
    public PedidoInvalidoException(string codigo, string mensagem)
        : base(mensagem)
    {
        Codigo = codigo;
    }

    public string Codigo { get; }
}
