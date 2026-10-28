using Microsoft.AspNetCore.Mvc;

namespace PizzariaByte.Mvc.Controllers;

/// <summary>
/// Página exibida pelo tratamento global de exceções (UseExceptionHandler) fora de Development.
/// </summary>
public sealed class ErroController : Controller
{
    public IActionResult Index() => View();
}
