namespace PizzariaByte.Combined.Domain;

/// <summary>
/// Item do cardápio. É imutável: depois de criada, uma pizza não muda durante a execução.
/// </summary>
public sealed class Pizza
{
    public Pizza(int id, string nome, string descricao, decimal preco)
    {
        // Estas verificações protegem contra erro de programação no cadastro inicial (seed),
        // e não contra erro do usuário — por isso usamos ArgumentException.
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome da pizza é obrigatório.", nameof(nome));
        }

        if (preco <= 0)
        {
            throw new ArgumentException("O preço da pizza deve ser positivo.", nameof(preco));
        }

        Id = id;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
    }

    public int Id { get; }

    public string Nome { get; }

    public string Descricao { get; }

    public decimal Preco { get; }
}
