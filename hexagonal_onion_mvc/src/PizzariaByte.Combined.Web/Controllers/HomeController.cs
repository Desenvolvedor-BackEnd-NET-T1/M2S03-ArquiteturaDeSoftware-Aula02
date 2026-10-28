using Microsoft.AspNetCore.Mvc;

namespace PizzariaByte.Combined.Web.Controllers;

public sealed class HomeController : Controller
{
    public IActionResult Index() => View();
}
