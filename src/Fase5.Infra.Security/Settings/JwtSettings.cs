namespace Fase5.Infra.Security.Settings;

public class JwtSettings
{
    /// <summary>
    /// Chave secreta antifalsificação do TOKEN
    /// </summary>
    public string? SecretKey { get; set; }
    /// <summary>
    /// Tempo de expiração do TOKEN em horas
    /// </summary>
    public int ExpirationInHours { get; set; }
}