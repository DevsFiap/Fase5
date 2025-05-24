using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IConsultaRepository : IBaseRepository<Consulta, Guid>
{
    Task<IEnumerable<Consulta>> ObterPorMedicoAsync(Guid medicoId);
    Task<IEnumerable<Consulta>> ObterPorPacienteAsync(Guid pacienteId);
}