using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IConsultaRepository : IBaseRepository<Consulta, Guid>
{
    Task<IEnumerable<Consulta>> GetConsultasByMedicoIdAsync(Guid medicoId);
    Task<IEnumerable<Consulta>> GetConsultasByPacienteIdAsync(Guid pacienteId);
}