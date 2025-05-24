using Fase5.Domain.Enuns;

namespace Fase5.Domain.Entities;

public class Consulta
{
    public Guid Id { get; set; }
    public Guid MedicoId { get; set; }
    public Medico? Medico { get; set; }
    public Guid PacienteId { get; set; }
    public Paciente? Paciente { get; set; }
    public DateTime DataHora { get; set; }
    public decimal Valor { get; set; }
    public StatusConsulta Status { get; set; } = StatusConsulta.Pendente;
    public string? JustificativaCancelamento { get; set; }
}