using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzariaByte.Onion.Domain.Entidades;
using PizzariaByte.Onion.Application.Pedidos;

namespace PizzariaByte.Onion.Web.Pages;

public sealed class CardapioModel : PageModel
{
    private readonly ConsultasPizzariaService _consultas;

    public CardapioModel(ConsultasPizzariaService consultas)
    {
        _consultas = consultas;
    }

    public IReadOnlyList<Pizza> Pizzas { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Pizzas = await _consultas.ListarCardapioAsync();
    }
}
