using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class MedicoDomainService(IUnitOfWork uow) : BaseDomainService<Medico, Guid>(uow.MedicoRepository), IMedicoDomainService
{
    public Task<Medico?> ObterPorCrmAsync(string crm)
        => uow.MedicoRepository.ObterPorCrmAsync(crm);

    public Task<IEnumerable<Medico>> BuscarPorEspecialidadeAsync(string especialidade)
        => uow.MedicoRepository.BuscarPorEspecialidadeAsync(especialidade);
}