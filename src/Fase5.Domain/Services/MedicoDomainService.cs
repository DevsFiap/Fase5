using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public sealed class MedicoDomainService(IUnitOfWork _unitOfWork) : BaseDomainService<Medico, Guid>(_unitOfWork.MedicoRepository), IMedicoDomainService
{
    public async Task<Medico?> ObterPorCrmAsync(string crm)
        => await _unitOfWork.MedicoRepository.ObterPorCrmAsync(crm);

    public async Task<IEnumerable<Medico>> BuscarPorEspecialidadeAsync(string esp)
        => await _unitOfWork.MedicoRepository.BuscarPorEspecialidadeAsync(esp);
}