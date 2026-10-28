using PizzariaByte.Hexagonal.Core.Dominio;
using PizzariaByte.Hexagonal.Core.Portas.Saida;

namespace PizzariaByte.Hexagonal.Adapters.Integracoes;

/// <summary>
/// Simulação: nenhuma chamada de rede. Para usar um serviço real, crie outra classe que
/// implemente <see cref="IDeliveryGateway"/> e troque o registro em Program.cs.
/// </summary>
public sealed class ApiDeliveryAiQFome(ConfiguracaoMock configuracao) : IDeliveryGateway
{
    public Task<string> SolicitarEntregaAsync(Pedido pedido)
    {
        if (configuracao.SimularFalha)
        {
            throw new IntegracaoException("ApiDeliveryAiQFome", "Serviço de entrega indisponível (falha simulada).");
        }

        var protocolo = $"AQF-{pedido.Id.ToString("N")[..8].ToUpperInvariant()}";
        Console.WriteLine($"[ApiDeliveryAiQFome] Entrega do pedido {pedido.Id} solicitada. Protocolo {protocolo} (simulado).");
        return Task.FromResult(protocolo);
    }
}
