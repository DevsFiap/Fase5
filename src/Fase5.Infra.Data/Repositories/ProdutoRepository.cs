using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public sealed class ProdutoRepository(DataContext ctx) : BaseRepository<Produto, int>(ctx), IProdutoRepository
{
    public async Task<IEnumerable<Produto>> BuscarDisponiveisAsync()
        => await ctx.Set<Produto>().Where(p => p.Disponivel).ToListAsync();

    public async Task<IEnumerable<Produto>> BuscarPorCategoriaAsync(CategoriaProduto categoria)
        => await ctx.Set<Produto>().Where(p => p.Categoria == categoria && p.Disponivel).ToListAsync();

    public Task<Produto?> ObterDisponivelPorIdAsync(int id)
        => ctx.Set<Produto>().FirstOrDefaultAsync(p => p.Id == id && p.Disponivel);

    public Task<bool> NomeExisteAsync(string nome)
        => ctx.Set<Produto>().AnyAsync(p => p.Nome == nome);
}