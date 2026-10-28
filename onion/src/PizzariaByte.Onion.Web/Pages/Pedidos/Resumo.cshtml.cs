using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzariaByte.Onion.Domain.Entidades;
using PizzariaByte.Onion.Application.Pedidos;

namespace PizzariaByte.Onion.Web.Pages.Pedidos;

public sealed class ResumoModel : PageModel
{
    private readonly ConsultasPizzariaService _consultas;

    public ResumoModel(ConsultasPizzariaService consultas)
    {
        _consultas = consultas;
    }

    public Pedido? Pedido { get; private set; }

    public async Task OnGetAsync(Guid id)
    {
        Pedido = await _consultas.ObterPedidoAsync(id);
    }
}
