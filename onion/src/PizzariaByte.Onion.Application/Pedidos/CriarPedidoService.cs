using PizzariaByte.Onion.Domain.Entidades;
using PizzariaByte.Onion.Domain.Excecoes;
using PizzariaByte.Onion.Domain.Repositorios;
using PizzariaByte.Onion.Application.Interfaces;

namespace PizzariaByte.Onion.Application.Pedidos;

/// <summary>
/// Caso de uso "realizar pedido de pizza". Orquestra validação, persistência e integrações,
/// mantendo a interface com o usuário enxuta.
/// </summary>
/// <remarks>
/// Política de consistência (sem transação):
/// 1. validar e criar o pedido (nada é salvo se algo estiver inválido);
/// 2. persistir com status Recebido;
/// 3. chamar e-mail e entrega, cada um isolado — a falha de um não impede o outro;
/// 4. definir o status final (Confirmado ou EntregaPendente) e atualizar a persistência.
/// Assim, nunca existe pedido notificado sem estar salvo, nem pedido "confirmado" sem entrega.
/// </remarks>
public sealed class CriarPedidoService
{
    private const string ConectorEmail = "ApiEmail";
    private const string ConectorDelivery = "ApiDeliveryAiQFome";

    private readonly ICardapioRepository _cardapio;
    private readonly IPedidoRepository _pedidos;
    private readonly IEmailGateway _email;
    private readonly IDeliveryGateway _delivery;
    private readonly IObservabilityGateway _observabilidade;

    // Todas as dependências chegam pelo construtor (injeção de dependência):
    // a Application conhece apenas contratos; as implementações ficam na Infrastructure.
    public CriarPedidoService(
        ICardapioRepository cardapio,
        IPedidoRepository pedidos,
        IEmailGateway email,
        IDeliveryGateway delivery,
        IObservabilityGateway observabilidade)
    {
        _cardapio = cardapio;
        _pedidos = pedidos;
        _email = email;
        _delivery = delivery;
        _observabilidade = observabilidade;
    }

    public async Task<ResultadoCriacaoPedido> CriarAsync(int pizzaId, int quantidade, string enderecoEntrega)
    {
        var pizza = await _cardapio.ObterPorIdAsync(pizzaId);
        if (pizza is null)
        {
            await RegistrarEventoComSegurancaAsync("PedidoRejeitado", $"Pizza {pizzaId} inexistente");
            return ResultadoCriacaoPedido.Falha("A pizza selecionada não existe no cardápio.");
        }

        Pedido pedido;
        try
        {
            pedido = Pedido.Criar(pizza, quantidade, enderecoEntrega);
        }
        catch (PedidoInvalidoException ex)
        {
            await RegistrarEventoComSegurancaAsync("PedidoRejeitado", ex.Codigo);
            return ResultadoCriacaoPedido.Falha(ex.Message);
        }

        await RegistrarEventoComSegurancaAsync(
            "PedidoCriado",
            $"Pedido {pedido.Id} — {pedido.Quantidade}x {pedido.NomePizza} — total {pedido.Total}");

        await _pedidos.AdicionarAsync(pedido);
        await RegistrarEventoComSegurancaAsync("PedidoPersistido", $"Pedido {pedido.Id}");

        await EnviarEmailAsync(pedido);
        var entregaSolicitada = await SolicitarEntregaAsync(pedido);

        if (entregaSolicitada)
        {
            pedido.Confirmar();
        }
        else
        {
            pedido.MarcarEntregaPendente();
        }

        await _pedidos.AtualizarAsync(pedido);
        await RegistrarEventoComSegurancaAsync("PedidoFinalizado", $"Pedido {pedido.Id} — status {pedido.Status}");

        return ResultadoCriacaoPedido.Ok(pedido);
    }

    private async Task EnviarEmailAsync(Pedido pedido)
    {
        try
        {
            await _email.EnviarConfirmacaoAsync(pedido);
            pedido.RegistrarIntegracao(ResultadoIntegracao.Ok(ConectorEmail, "Confirmação enviada por e-mail."));
        }
        catch (Exception ex)
        {
            // catch amplo intencional: esta é a fronteira com um sistema externo.
            // Qualquer falha técnica vira um resultado de integração, não um erro do pedido.
            pedido.RegistrarIntegracao(ResultadoIntegracao.Falha(ConectorEmail, "Não foi possível enviar o e-mail de confirmação."));
            await RegistrarEventoComSegurancaAsync("FalhaIntegracao", $"{ConectorEmail}: {ex.Message}");
        }
    }

    private async Task<bool> SolicitarEntregaAsync(Pedido pedido)
    {
        try
        {
            var protocolo = await _delivery.SolicitarEntregaAsync(pedido);
            pedido.RegistrarIntegracao(ResultadoIntegracao.Ok(ConectorDelivery, $"Entrega solicitada. Protocolo: {protocolo}."));
            return true;
        }
        catch (Exception ex)
        {
            // Mesmo raciocínio do e-mail: o pedido continua salvo, com entrega pendente.
            pedido.RegistrarIntegracao(ResultadoIntegracao.Falha(
                ConectorDelivery,
                "Não foi possível solicitar a entrega. O pedido ficou com entrega pendente."));
            await RegistrarEventoComSegurancaAsync("FalhaIntegracao", $"{ConectorDelivery}: {ex.Message}");
            return false;
        }
    }

    // A observabilidade é "best-effort": se ela falhar, o pedido não pode ser prejudicado.
    private async Task RegistrarEventoComSegurancaAsync(string evento, string detalhe)
    {
        try
        {
            await _observabilidade.RegistrarEventoAsync(evento, detalhe);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Aviso] Falha ao registrar observabilidade: {ex.Message}");
        }
    }
}
