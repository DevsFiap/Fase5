using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IMedicoRepository : IBaseRepository<Medico, Guid>
{
    Task<Medico?> ObterPorCrmAsync(string crm);
    Task<IEnumerable<Medico>> BuscarPorEspecialidadeAsync(string especialidade);
}