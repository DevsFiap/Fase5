using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class HorarioDisponivelRepository(DataContext _dataContext) : BaseRepository<HorarioDisponivel, Guid>(_dataContext), IHorarioDisponivelRepository
{
    public async Task<bool> ExisteChoqueDeHorarioAsync(Guid medicoId, DateTime inicio, DateTime fim)
        => await _dataContext.Set<HorarioDisponivel>().AnyAsync(h => h.MedicoId == medicoId && h.Inicio < fim && h.Fim > inicio);
}