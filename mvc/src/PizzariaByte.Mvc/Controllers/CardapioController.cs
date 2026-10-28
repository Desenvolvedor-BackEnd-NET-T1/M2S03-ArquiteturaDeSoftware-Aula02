using Microsoft.AspNetCore.Mvc;
using PizzariaByte.Mvc.Services;

namespace PizzariaByte.Mvc.Controllers;

public sealed class CardapioController : Controller
{
    private readonly ConsultasPizzariaService _consultas;

    public CardapioController(ConsultasPizzariaService consultas)
    {
        _consultas = consultas;
    }

    public async Task<IActionResult> Index() => View(await _consultas.ListarCardapioAsync());
}
