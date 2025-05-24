using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public class PacienteDomainService(IUnitOfWork _unitOfWork) : BaseDomainService<Paciente, Guid>(_unitOfWork.PacienteRepository), IPacienteDomainService
{
    public async Task<Paciente?> ObterPorCpfAsync(string cpf)
        => await _unitOfWork.PacienteRepository.ObterPorCpfAsync(cpf);
}