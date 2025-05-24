using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class ConsultaRepository(DataContext _dataContext) : BaseRepository<Consulta, Guid>(_dataContext), IConsultaRepository
{
    public async Task<IEnumerable<Consulta>> ObterPorMedicoAsync(Guid medicoId)
        => await _dataContext.Set<Consulta>().AsNoTracking().Where(c => c.MedicoId == medicoId).ToListAsync();

    public async Task<IEnumerable<Consulta>> ObterPorPacienteAsync(Guid pacienteId)
        => await _dataContext.Set<Consulta>().AsNoTracking().Where(c => c.PacienteId == pacienteId).ToListAsync();
}