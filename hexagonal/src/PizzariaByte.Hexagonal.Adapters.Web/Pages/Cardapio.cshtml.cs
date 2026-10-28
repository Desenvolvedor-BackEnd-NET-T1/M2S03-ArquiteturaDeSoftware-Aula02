using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzariaByte.Hexagonal.Core.Dominio;
using PizzariaByte.Hexagonal.Core.Portas.Entrada;

namespace PizzariaByte.Hexagonal.Adapters.Web.Pages;

public sealed class CardapioModel : PageModel
{
    private readonly IConsultasPizzariaUseCase _consultas;

    public CardapioModel(IConsultasPizzariaUseCase consultas)
    {
        _consultas = consultas;
    }

    public IReadOnlyList<Pizza> Pizzas { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Pizzas = await _consultas.ListarCardapioAsync();
    }
}
