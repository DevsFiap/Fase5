using Fase5.Domain.Enuns;

namespace Fase5.Domain.Entities;

public class Funcionario
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }

    public CargoFuncionario Cargo { get; set; }
    public PerfilUsuario Perfil { get; private set; } = PerfilUsuario.Funcionario;
}