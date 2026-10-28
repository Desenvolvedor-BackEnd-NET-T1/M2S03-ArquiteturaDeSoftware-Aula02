using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Application.Ports.Entrada;

/// <summary>
/// Porta de entrada (Hexagonal), definida no anel de aplicação (Onion) para as consultas exibidas nas telas: cardápio e resumo do pedido.
/// </summary>
public interface IConsultasPizzariaUseCase
{
    Task<IReadOnlyList<Pizza>> ListarCardapioAsync();

    Task<Pizza?> ObterPizzaAsync(int id);

    Task<Pedido?> ObterPedidoAsync(Guid id);
}
