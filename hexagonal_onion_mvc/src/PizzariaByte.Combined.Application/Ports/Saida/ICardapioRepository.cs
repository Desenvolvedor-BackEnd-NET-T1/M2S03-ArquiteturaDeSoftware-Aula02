using PizzariaByte.Combined.Domain;

namespace PizzariaByte.Combined.Application.Ports.Saida;

/// <summary>
/// Porta de saída (Hexagonal), definida no anel de aplicação (Onion): leitura do cardápio.
/// </summary>
public interface ICardapioRepository
{
    Task<IReadOnlyList<Pizza>> ListarAsync();

    Task<Pizza?> ObterPorIdAsync(int id);
}
