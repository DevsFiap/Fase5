namespace Fase5.Domain.Entities;

public class HorarioDisponivel
{
    public Guid Id { get; set; }
    public Guid MedicoId { get; set; }
    public Medico? Medico { get; set; }

    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public bool Ocupado { get; set; }
}