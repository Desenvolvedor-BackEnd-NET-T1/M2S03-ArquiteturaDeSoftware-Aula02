using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzariaByte.Onion.Domain.Entidades;
using PizzariaByte.Onion.Application.Pedidos;

namespace PizzariaByte.Onion.Web.Pages.Pedidos;

/// <summary>
/// Camada de apresentação (anel externo): recebe o formulário e chama o serviço de aplicação
/// <see cref="CriarPedidoService"/>. Na Onion, o anel externo pode usar a classe concreta do anel
/// interno: o que importa é que as dependências apontem para dentro, não que tudo tenha interface.
/// </summary>
public sealed class NovoModel : PageModel
{
    private readonly CriarPedidoService _criarPedido;
    private readonly ConsultasPizzariaService _consultas;

    public NovoModel(CriarPedidoService criarPedido, ConsultasPizzariaService consultas)
    {
        _criarPedido = criarPedido;
        _consultas = consultas;
    }

    [BindProperty]
    public NovoPedidoInput Entrada { get; set; } = new();

    public IReadOnlyList<Pizza> Cardapio { get; private set; } = [];

    public async Task OnGetAsync(int? pizzaId)
    {
        Entrada = new NovoPedidoInput { PizzaId = pizzaId, Quantidade = 1 };
        Cardapio = await _consultas.ListarCardapioAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Cardapio = await _consultas.ListarCardapioAsync();
            return Page();
        }

        var resultado = await _criarPedido.CriarAsync(Entrada.PizzaId!.Value, Entrada.Quantidade!.Value, Entrada.EnderecoEntrega!);
        if (!resultado.Sucesso)
        {
            foreach (var erro in resultado.Erros)
            {
                ModelState.AddModelError(string.Empty, erro);
            }

            Cardapio = await _consultas.ListarCardapioAsync();
            return Page();
        }

        // Post-Redirect-Get: redirecionar evita criar outro pedido ao apertar F5.
        return RedirectToPage("/Pedidos/Resumo", new { id = resultado.Pedido!.Id });
    }

    /// <summary>
    /// Dados do formulário. As validações são uma primeira barreira amigável;
    /// a regra de verdade está no domínio (Pedido.Criar).
    /// </summary>
    public sealed class NovoPedidoInput
    {
        [Required(ErrorMessage = "Escolha uma pizza.")]
        [Display(Name = "Pizza")]
        public int? PizzaId { get; set; }

        [Required(ErrorMessage = "Informe a quantidade.")]
        [Range(1, 50, ErrorMessage = "A quantidade deve estar entre 1 e 50.")]
        public int? Quantidade { get; set; }

        [Required(ErrorMessage = "Informe o endereço de entrega.")]
        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres.")]
        [Display(Name = "Endereço de entrega")]
        public string? EnderecoEntrega { get; set; }
    }
}
