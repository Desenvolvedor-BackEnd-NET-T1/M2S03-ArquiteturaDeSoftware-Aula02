using Microsoft.AspNetCore.Mvc;
using PizzariaByte.Combined.Application.Ports.Entrada;

namespace PizzariaByte.Combined.Web.Controllers;

public sealed class CardapioController : Controller
{
    private readonly IConsultasPizzariaUseCase _consultas;

    public CardapioController(IConsultasPizzariaUseCase consultas)
    {
        _consultas = consultas;
    }

    public async Task<IActionResult> Index() => View(await _consultas.ListarCardapioAsync());
}
