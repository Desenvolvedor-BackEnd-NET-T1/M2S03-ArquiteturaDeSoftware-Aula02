using PizzariaByte.Hexagonal.Core.Dominio;

namespace PizzariaByte.Hexagonal.Core.Portas.Entrada;

/// <summary>
/// Porta de entrada para as consultas exibidas nas telas: cardápio e resumo do pedido.
/// </summary>
public interface IConsultasPizzariaUseCase
{
    Task<IReadOnlyList<Pizza>> ListarCardapioAsync();

    Task<Pizza?> ObterPizzaAsync(int id);

    Task<Pedido?> ObterPedidoAsync(Guid id);
}
