using Microsoft.AspNetCore.Mvc;
using PizzariaByte.Mvc.Models;
using PizzariaByte.Mvc.Services;

namespace PizzariaByte.Mvc.Controllers;

/// <summary>
/// Controller: recebe a requisição, valida a entrada da tela, delega ao serviço
/// e escolhe qual View mostrar. Não contém regra de negócio nem persistência.
/// </summary>
public sealed class PedidosController : Controller
{
    private readonly CriarPedidoService _criarPedido;
    private readonly ConsultasPizzariaService _consultas;

    public PedidosController(CriarPedidoService criarPedido, ConsultasPizzariaService consultas)
    {
        _criarPedido = criarPedido;
        _consultas = consultas;
    }

    [HttpGet]
    public async Task<IActionResult> Novo(int? pizzaId)
    {
        var modelo = new NovoPedidoViewModel
        {
            PizzaId = pizzaId,
            Quantidade = 1,
            Cardapio = await _consultas.ListarCardapioAsync()
        };

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Novo(NovoPedidoViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            modelo.Cardapio = await _consultas.ListarCardapioAsync();
            return View(modelo);
        }

        var resultado = await _criarPedido.CriarAsync(modelo.PizzaId!.Value, modelo.Quantidade!.Value, modelo.EnderecoEntrega!);
        if (!resultado.Sucesso)
        {
            foreach (var erro in resultado.Erros)
            {
                ModelState.AddModelError(string.Empty, erro);
            }

            modelo.Cardapio = await _consultas.ListarCardapioAsync();
            return View(modelo);
        }

        // Post-Redirect-Get: redirecionar evita criar outro pedido ao apertar F5.
        return RedirectToAction(nameof(Resumo), new { id = resultado.Pedido!.Id });
    }

    [HttpGet("Pedidos/{id:guid}")]
    public async Task<IActionResult> Resumo(Guid id)
    {
        var pedido = await _consultas.ObterPedidoAsync(id);
        return pedido is null ? View("NaoEncontrado") : View(pedido);
    }
}
