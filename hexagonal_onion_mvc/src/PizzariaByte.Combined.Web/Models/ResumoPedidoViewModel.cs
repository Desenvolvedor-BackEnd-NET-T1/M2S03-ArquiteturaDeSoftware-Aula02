using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Web.Models;

/// <summary>
/// Model de apresentação do resumo. No MVC puro, a View recebe o próprio Model de domínio;
/// aqui o domínio vive no centro da Onion, então a camada MVC usa um ViewModel próprio e
/// não duplica nenhuma regra — apenas copia e formata o que a View precisa mostrar.
/// </summary>
public sealed class ResumoPedidoViewModel
{
    public Guid Id { get; init; }

    public string NomePizza { get; init; } = string.Empty;

    public int Quantidade { get; init; }

    public decimal PrecoUnitario { get; init; }

    public decimal Subtotal { get; init; }

    public decimal TaxaEntrega { get; init; }

    public decimal Total { get; init; }

    public string EnderecoEntrega { get; init; } = string.Empty;

    public string StatusDescricao { get; init; } = string.Empty;

    public bool PossuiFalhaDeIntegracao { get; init; }

    public IReadOnlyList<ResultadoIntegracao> Integracoes { get; init; } = [];

    public static ResumoPedidoViewModel De(Pedido pedido) => new()
    {
        Id = pedido.Id,
        NomePizza = pedido.NomePizza,
        Quantidade = pedido.Quantidade,
        PrecoUnitario = pedido.PrecoUnitario,
        Subtotal = pedido.Subtotal,
        TaxaEntrega = pedido.TaxaEntrega,
        Total = pedido.Total,
        EnderecoEntrega = pedido.EnderecoEntrega,
        StatusDescricao = pedido.Status switch
        {
            StatusPedido.Recebido => "Recebido",
            StatusPedido.Confirmado => "Confirmado",
            StatusPedido.EntregaPendente => "Entrega pendente",
            _ => pedido.Status.ToString()
        },
        PossuiFalhaDeIntegracao = pedido.PossuiFalhaDeIntegracao,
        Integracoes = pedido.Integracoes
    };
}
