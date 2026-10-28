using PizzariaByte.Hexagonal.Core.Dominio;

namespace PizzariaByte.Hexagonal.Core.Portas.Saida;

/// <summary>
/// Porta de saída: leitura do cardápio.
/// </summary>
public interface ICardapioRepository
{
    Task<IReadOnlyList<Pizza>> ListarAsync();

    Task<Pizza?> ObterPorIdAsync(int id);
}
