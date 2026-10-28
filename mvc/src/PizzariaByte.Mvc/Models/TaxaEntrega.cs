namespace PizzariaByte.Mvc.Models;

/// <summary>
/// Regra da taxa de entrega, idêntica nos quatro projetos:
/// R$ 8,00 por pedido, isenta quando o subtotal for maior ou igual a R$ 100,00.
/// </summary>
/// <remarks>
/// A classe é estática porque é uma função pura: não guarda estado e não depende de nada externo.
/// Isso não é um atalho para evitar injeção de dependência — não há o que injetar aqui.
/// </remarks>
public static class TaxaEntrega
{
    public const decimal ValorPadrao = 8.00m;

    public const decimal SubtotalMinimoParaIsencao = 100.00m;

    public static decimal Calcular(decimal subtotal) =>
        subtotal >= SubtotalMinimoParaIsencao ? 0m : ValorPadrao;
}
