using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class MedicoRepository(DataContext _dataContext) : BaseRepository<Medico, Guid>(_dataContext), IMedicoRepository
{
    public async Task<Medico?> ObterPorCrmAsync(string crm)
        => await _dataContext.Set<Medico>().AsNoTracking().FirstOrDefaultAsync(m => m.CRM == crm);

    public async Task<List<Medico>> BuscarPorEspecialidadeAsync(string esp)
        => await _dataContext.Set<Medico>().AsNoTracking().Where(m => m.Especialidade == esp).ToListAsync();
}