using PizzariaByte.Onion.Domain.Entidades;

namespace PizzariaByte.Onion.Domain.Repositorios;

/// <summary>
/// Contrato de repositório do domínio (fica no centro porque descreve coleções de entidades): leitura do cardápio.
/// </summary>
public interface ICardapioRepository
{
    Task<IReadOnlyList<Pizza>> ListarAsync();

    Task<Pizza?> ObterPorIdAsync(int id);
}
