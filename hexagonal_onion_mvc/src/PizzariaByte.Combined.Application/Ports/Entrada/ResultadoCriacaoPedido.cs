using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Application.Ports.Entrada;

/// <summary>
/// Resposta explícita do serviço de criação: sucesso (com o pedido) ou falha (com mensagens).
/// Assim a interface com o usuário não precisa capturar exceções de negócio.
/// </summary>
public sealed class ResultadoCriacaoPedido
{
    private ResultadoCriacaoPedido(bool sucesso, Pedido? pedido, IReadOnlyList<string> erros)
    {
        Sucesso = sucesso;
        Pedido = pedido;
        Erros = erros;
    }

    public bool Sucesso { get; }

    public Pedido? Pedido { get; }

    public IReadOnlyList<string> Erros { get; }

    public static ResultadoCriacaoPedido Ok(Pedido pedido) => new(true, pedido, []);

    public static ResultadoCriacaoPedido Falha(string erro) => new(false, null, [erro]);
}
