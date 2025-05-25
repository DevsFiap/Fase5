using Fase5.Domain.Core;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public abstract class BaseRepository<TEntity, TKey>(DataContext ctx)
    : IBaseRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DataContext _ctx = ctx;

    public virtual async Task<List<TEntity>> GetAllAsync()
        => await _ctx.Set<TEntity>().AsNoTracking().ToListAsync();

    public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        => await _ctx.Set<TEntity>().FindAsync(id);

    public virtual Task CreateAsync(TEntity entity)
    {
        _ctx.Set<TEntity>().Add(entity);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        _ctx.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        _ctx.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public void Dispose() 
        => _ctx.Dispose();
}