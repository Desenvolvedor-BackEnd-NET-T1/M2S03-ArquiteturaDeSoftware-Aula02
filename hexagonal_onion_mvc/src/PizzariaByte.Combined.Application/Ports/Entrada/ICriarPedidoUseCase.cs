namespace PizzariaByte.Combined.Application.Ports.Entrada;

/// <summary>
/// Porta de entrada (Hexagonal), definida no anel de aplicação (Onion): o que o mundo externo (ex.: a interface web) pode pedir ao núcleo.
/// Qualquer adaptador de entrada — web, API, CLI ou um teste — usa esta mesma porta.
/// </summary>
public interface ICriarPedidoUseCase
{
    Task<ResultadoCriacaoPedido> CriarAsync(int pizzaId, int quantidade, string enderecoEntrega);
}
