using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Domain.Interfaces.Services;

namespace Fase5.Domain.Services;

public class HorarioDisponivelDomainService(IUnitOfWork uow)
    : BaseDomainService<HorarioDisponivel, Guid>(uow.HorarioDisponivelRepository),
      IHorarioDisponivelDomainService
{
    public Task<HorarioDisponivel?> ObterHorarioDisponivelAsync(Guid medicoId, DateTime dataHora)
        => uow.HorarioDisponivelRepository.ObterHorarioDisponivelAsync(medicoId, dataHora);

    public Task<bool> ExisteChoqueDeHorarioAsync(Guid medicoId, DateTime inicio, DateTime fim)
        => uow.HorarioDisponivelRepository.ExisteChoqueDeHorarioAsync(medicoId, inicio, fim);
}