using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public class FuncionarioRepository(DataContext ctx) : BaseRepository<Funcionario, int>(ctx), IFuncionarioRepository
{
    public Task<Funcionario?> ObterPorEmailAsync(string email)
        => ctx.Set<Funcionario>().FirstOrDefaultAsync(f => f.Email == email);

    public async Task<IEnumerable<Funcionario>> BuscarPorCargoAsync(CargoFuncionario cargo)
        => await ctx.Set<Funcionario>().Where(f => f.Cargo == cargo).ToListAsync();

    public Task<bool> EmailExisteAsync(string email)
        => ctx.Set<Funcionario>().AnyAsync(f => f.Email == email);

    public Task<bool> ExisteCargoAsync(CargoFuncionario cargo) 
        => _ctx.Set<Funcionario>().AnyAsync(f => f.Cargo == cargo);
}