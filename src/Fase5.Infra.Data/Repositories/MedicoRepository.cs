using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class MedicoRepository(DataContext ctx) : BaseRepository<Medico, Guid>(ctx), IMedicoRepository
{
    public Task<Medico?> ObterPorCrmAsync(string crm)
        => ctx.Set<Medico>()
              .FirstOrDefaultAsync(m => m.CRM == crm);

    public async Task<IEnumerable<Medico>> BuscarPorEspecialidadeAsync(string especialidade)
        => await ctx.Set<Medico>()
                    .Where(m => m.Especialidade == especialidade)
                    .ToListAsync();
}