using PizzariaByte.Mvc.Models;

namespace PizzariaByte.Mvc.Data;

/// <summary>
/// Abstração de acesso ao cardápio. O serviço de pedidos depende desta interface,
/// e não da classe em memória, para que a implementação possa ser trocada via DI.
/// </summary>
public interface ICardapioRepository
{
    Task<IReadOnlyList<Pizza>> ListarAsync();

    Task<Pizza?> ObterPorIdAsync(int id);
}
