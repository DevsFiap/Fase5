using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class PacienteRepository(DataContext ctx) : BaseRepository<Paciente, Guid>(ctx), IPacienteRepository
{
    public Task<Paciente?> ObterPorCpfAsync(string cpf)
        => ctx.Set<Paciente>().FirstOrDefaultAsync(p => p.CPF == cpf);
}