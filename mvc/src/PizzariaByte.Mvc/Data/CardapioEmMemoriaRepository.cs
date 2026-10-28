using PizzariaByte.Mvc.Models;

namespace PizzariaByte.Mvc.Data;

/// <summary>
/// Cardápio em memória: dados previsíveis, recriados a cada inicialização da aplicação.
/// </summary>
public sealed class CardapioEmMemoriaRepository : ICardapioRepository
{
    private readonly IReadOnlyList<Pizza> _pizzas =
    [
        new Pizza(1, "Margherita", "Molho de tomate, muçarela e manjericão", 45.00m),
        new Pizza(2, "Calabresa", "Molho de tomate, muçarela, calabresa e cebola", 50.00m),
        new Pizza(3, "Quatro Queijos", "Muçarela, provolone, parmesão e gorgonzola", 55.00m)
    ];

    public Task<IReadOnlyList<Pizza>> ListarAsync() => Task.FromResult(_pizzas);

    public Task<Pizza?> ObterPorIdAsync(int id) => Task.FromResult(_pizzas.FirstOrDefault(p => p.Id == id));
}
