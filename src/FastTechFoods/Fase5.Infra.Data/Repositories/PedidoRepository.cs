using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using Fase5.Domain.Interfaces.Repositories;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public sealed class PedidoRepository(DataContext ctx) : BaseRepository<Pedido, int>(ctx), IPedidoRepository
{
    public Task<Pedido?> ObterDetalhadoAsync(int id)
        => ctx.Set<Pedido>().Include(p => p.Itens).ThenInclude(i => i.Produto).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Pedido>> ObterPorClienteAsync(int clienteId)
        => await ctx.Set<Pedido>().Where(p => p.ClienteId == clienteId).OrderByDescending(p => p.CriadoEm).ToListAsync();

    public async Task<IEnumerable<Pedido>> ObterPorStatusAsync(StatusPedido status)
        => await ctx.Set<Pedido>().Where(p => p.Status == status).OrderBy(p => p.CriadoEm).ToListAsync();
}