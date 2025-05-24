using Fase5.Domain.Enuns;

namespace Fase5.Domain.Entities;

public class Paciente : Usuario
{
    public string? CPF { get; set; }
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

    public Paciente() => Perfil = PerfilUsuario.Paciente;
}