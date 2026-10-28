namespace PizzariaByte.Hexagonal.Adapters.Integracoes;

/// <summary>
/// Configuração dos conectores simulados, lida do appsettings.json em Program.cs.
/// </summary>
public sealed record ConfiguracaoMock(bool SimularFalha);
