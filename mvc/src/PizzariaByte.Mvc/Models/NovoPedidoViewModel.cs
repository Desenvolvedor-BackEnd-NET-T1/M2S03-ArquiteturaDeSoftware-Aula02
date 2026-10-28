using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PizzariaByte.Mvc.Models;

/// <summary>
/// Dados do formulário de novo pedido. As validações abaixo são uma primeira barreira
/// amigável para a tela; a regra de verdade está no domínio (<see cref="Pedido.Criar"/>).
/// </summary>
public sealed class NovoPedidoViewModel
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

    /// <summary>Usado apenas para montar a lista de pizzas; nunca vem do POST.</summary>
    [BindNever]
    public IReadOnlyList<Pizza> Cardapio { get; set; } = [];
}
