using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PizzariaByte.Onion.Web.Pages;

/// <summary>
/// Página exibida pelo tratamento global de exceções (UseExceptionHandler) fora de Development.
/// </summary>
public sealed class ErroModel : PageModel
{
    public void OnGet()
    {
    }
}
