using Fase5.Domain.Entities;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public sealed class ClienteRepository(DataContext ctx) : BaseRepository<Cliente, int>(ctx), IClienteRepository
{
    public Task<Cliente?> ObterPorEmailAsync(string email)
        => ctx.Set<Cliente>().FirstOrDefaultAsync(c => c.Email == email);

    public Task<Cliente?> ObterPorCpfAsync(string cpf)
        => ctx.Set<Cliente>().FirstOrDefaultAsync(c => c.CPF == cpf);

    public Task<bool> EmailExisteAsync(string email)
        => ctx.Set<Cliente>().AnyAsync(c => c.Email == email);

    public Task<bool> CpfExisteAsync(string cpf)
        => ctx.Set<Cliente>().AnyAsync(c => c.CPF == cpf);
}