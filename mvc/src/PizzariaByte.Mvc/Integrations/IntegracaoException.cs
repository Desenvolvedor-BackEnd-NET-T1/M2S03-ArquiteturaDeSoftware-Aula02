namespace PizzariaByte.Mvc.Integrations;

/// <summary>
/// Falha técnica de um conector externo. Um conector real também deveria encapsular
/// seus erros (timeout, HTTP 500...) neste tipo.
/// </summary>
public sealed class IntegracaoException : Exception
{
    public IntegracaoException(string conector, string mensagem)
        : base(mensagem)
    {
        Conector = conector;
    }

    public string Conector { get; }
}
