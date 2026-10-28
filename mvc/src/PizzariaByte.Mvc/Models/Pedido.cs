using RegraTaxaEntrega = PizzariaByte.Mvc.Models.TaxaEntrega;

namespace PizzariaByte.Mvc.Models;

/// <summary>
/// Pedido de pizza. No MVC, o Model concentra os dados e as regras pertinentes a eles:
/// é aqui que ficam as validações de quantidade e endereço e o cálculo do total.
/// </summary>
public sealed class Pedido
{
    public const int QuantidadeMaxima = 50;

    public const int TamanhoMaximoEndereco = 200;

    private readonly List<ResultadoIntegracao> _integracoes = [];

    // Construtor privado: a única forma de obter um Pedido é pela fábrica Criar,
    // que garante que nenhum pedido inválido exista.
    private Pedido()
    {
    }

    public Guid Id { get; private set; }

    public int PizzaId { get; private set; }

    public string NomePizza { get; private set; } = string.Empty;

    public decimal PrecoUnitario { get; private set; }

    public int Quantidade { get; private set; }

    public string EnderecoEntrega { get; private set; } = string.Empty;

    public decimal Subtotal { get; private set; }

    public decimal TaxaEntrega { get; private set; }

    public decimal Total { get; private set; }

    public StatusPedido Status { get; private set; }

    public IReadOnlyList<ResultadoIntegracao> Integracoes => _integracoes;

    public bool PossuiFalhaDeIntegracao => _integracoes.Any(i => !i.Sucesso);

    public static Pedido Criar(Pizza pizza, int quantidade, string enderecoEntrega)
    {
        ArgumentNullException.ThrowIfNull(pizza);

        if (quantidade <= 0)
        {
            throw new PedidoInvalidoException("QUANTIDADE_INVALIDA", "A quantidade deve ser maior que zero.");
        }

        if (quantidade > QuantidadeMaxima)
        {
            throw new PedidoInvalidoException("QUANTIDADE_ACIMA_DO_LIMITE", "A quantidade máxima por pedido é 50.");
        }

        var endereco = enderecoEntrega?.Trim() ?? string.Empty;
        if (endereco.Length == 0)
        {
            throw new PedidoInvalidoException("ENDERECO_OBRIGATORIO", "Informe o endereço de entrega.");
        }

        if (endereco.Length > TamanhoMaximoEndereco)
        {
            throw new PedidoInvalidoException("ENDERECO_MUITO_LONGO", "O endereço deve ter no máximo 200 caracteres.");
        }

        // O preço é copiado para o pedido: o total fica estável mesmo que o cardápio mude.
        var subtotal = pizza.Preco * quantidade;
        var taxa = RegraTaxaEntrega.Calcular(subtotal);

        return new Pedido
        {
            Id = Guid.NewGuid(),
            PizzaId = pizza.Id,
            NomePizza = pizza.Nome,
            PrecoUnitario = pizza.Preco,
            Quantidade = quantidade,
            EnderecoEntrega = endereco,
            Subtotal = subtotal,
            TaxaEntrega = taxa,
            Total = subtotal + taxa,
            Status = StatusPedido.Recebido
        };
    }

    public void RegistrarIntegracao(ResultadoIntegracao resultado)
    {
        ArgumentNullException.ThrowIfNull(resultado);
        _integracoes.Add(resultado);
    }

    public void Confirmar() => Status = StatusPedido.Confirmado;

    public void MarcarEntregaPendente() => Status = StatusPedido.EntregaPendente;
}
