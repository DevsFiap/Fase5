using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Services;

public interface IHorarioDisponivelDomainService : IBaseDomainService<HorarioDisponivel, Guid>
{
    Task<HorarioDisponivel?> ObterHorarioDisponivelAsync(Guid medicoId, DateTime dataHora);
    Task<bool> ExisteChoqueDeHorarioAsync(Guid medicoId, DateTime inicio, DateTime fim);
}