namespace PizzariaByte.Mvc.Models;

/// <summary>
/// Resultado de uma chamada a um conector externo, exibido no resumo do pedido.
/// </summary>
public sealed record ResultadoIntegracao(string Conector, bool Sucesso, string Mensagem)
{
    public static ResultadoIntegracao Ok(string conector, string mensagem) => new(conector, true, mensagem);

    public static ResultadoIntegracao Falha(string conector, string mensagem) => new(conector, false, mensagem);
}
