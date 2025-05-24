using Fase5.Domain.Core;
using Fase5.Domain.Entities;

namespace Fase5.Domain.Interfaces.Repositories;

public interface IHorarioDisponivelRepository : IBaseRepository<HorarioDisponivel, Guid>
{
    Task<bool> ExisteChoqueDeHorarioAsync(Guid medicoId, DateTime inicio, DateTime fim);
}