using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IMedicoDomainService : IBaseDomainService<Medico, Guid>
{
    Task<Medico?> ObterPorCrmAsync(string crm);
    Task<IEnumerable<Medico>> BuscarPorEspecialidadeAsync(string especialidade);
}