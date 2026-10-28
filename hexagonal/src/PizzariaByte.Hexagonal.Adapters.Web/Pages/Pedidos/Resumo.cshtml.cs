using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzariaByte.Hexagonal.Core.Dominio;
using PizzariaByte.Hexagonal.Core.Portas.Entrada;

namespace PizzariaByte.Hexagonal.Adapters.Web.Pages.Pedidos;

public sealed class ResumoModel : PageModel
{
    private readonly IConsultasPizzariaUseCase _consultas;

    public ResumoModel(IConsultasPizzariaUseCase consultas)
    {
        _consultas = consultas;
    }

    public Pedido? Pedido { get; private set; }

    public async Task OnGetAsync(Guid id)
    {
        Pedido = await _consultas.ObterPedidoAsync(id);
    }
}
