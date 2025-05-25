using Fase5.Domain.Enuns;

namespace Fase5.Domain.Entities;

public abstract class Usuario
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Login { get; set; }
    public string? Senha { get; set; }
    public PerfilUsuario Perfil { get; set; }
}