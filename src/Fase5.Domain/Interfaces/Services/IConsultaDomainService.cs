using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IConsultaDomainService : IBaseDomainService<Consulta, Guid>
{
    Task<IEnumerable<Consulta>> ObterPorMedicoAsync(Guid medicoId);
    Task<IEnumerable<Consulta>> ObterPorPacienteAsync(Guid pacienteId);
}