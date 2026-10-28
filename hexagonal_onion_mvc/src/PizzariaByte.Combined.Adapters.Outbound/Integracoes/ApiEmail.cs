using PizzariaByte.Combined.Domain;
using PizzariaByte.Combined.Application.Ports.Saida;

namespace PizzariaByte.Combined.Adapters.Outbound.Integracoes;

/// <summary>
/// Simulação: nenhuma chamada de rede. Para usar um serviço real, crie outra classe que
/// implemente <see cref="IEmailGateway"/> e troque o registro em Program.cs.
/// </summary>
public sealed class ApiEmail(ConfiguracaoMock configuracao) : IEmailGateway
{
    public Task EnviarConfirmacaoAsync(Pedido pedido)
    {
        if (configuracao.SimularFalha)
        {
            throw new IntegracaoException("ApiEmail", "Servidor de e-mail indisponível (falha simulada).");
        }

        Console.WriteLine($"[ApiEmail] Confirmação do pedido {pedido.Id} enviada (simulado).");
        return Task.CompletedTask;
    }
}
