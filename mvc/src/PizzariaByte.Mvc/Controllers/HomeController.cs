using Microsoft.AspNetCore.Mvc;

namespace PizzariaByte.Mvc.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index() => View();
}
