using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public class HorarioDisponivelDomainService(IUnitOfWork _unitOfWork)
    : BaseDomainService<HorarioDisponivel, Guid>(_unitOfWork.HorarioDisponivelRepository)
    , IHorarioDisponivelDomainService
{
    public async Task<bool> ExisteChoqueDeHorarioAsync(Guid medicoId, DateTime inicio, DateTime fim)
        => await _unitOfWork.HorarioDisponivelRepository.ExisteChoqueDeHorarioAsync(medicoId, inicio, fim);
}