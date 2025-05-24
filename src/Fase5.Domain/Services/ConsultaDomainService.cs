using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public class ConsultaDomainService(IUnitOfWork _unitOfWork) : BaseDomainService<Consulta, Guid>(_unitOfWork.ConsultaRepository), IConsultaDomainService
{
    public async Task<IEnumerable<Consulta>> ObterPorMedicoAsync(Guid medicoId)
        => await _unitOfWork.ConsultaRepository.ObterPorMedicoAsync(medicoId);

    public async Task<IEnumerable<Consulta>> ObterPorPacienteAsync(Guid pacienteId)
        => await _unitOfWork.ConsultaRepository.ObterPorPacienteAsync(pacienteId);
}