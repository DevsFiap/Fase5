using Fase5.Domain.Enuns;

namespace Fase5.Domain.Entities;

public class Medico : Usuario
{
    public string? CRM { get; set; }
    public string? Especialidade { get; set; }
    public decimal ValorConsultaPadrao { get; set; }

    public ICollection<HorarioDisponivel> HorariosDisponiveis { get; set; } = new List<HorarioDisponivel>();
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

    public Medico() => Perfil = PerfilUsuario.Medico;
}