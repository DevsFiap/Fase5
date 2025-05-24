using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class PacienteRepository(DataContext _dataContext) : BaseRepository<Paciente, Guid>(_dataContext), IPacienteRepository
{
    public async Task<Paciente?> ObterPorCpfAsync(string cpf)
        => await _dataContext.Set<Paciente>().AsNoTracking().FirstOrDefaultAsync(p => p.CPF == cpf);
}