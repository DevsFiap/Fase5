using Fase5.Domain.Core;
using Fase5.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fase5.Infra.Data.Repositories;

public abstract class BaseRepository<TEntity, TKey>(DataContext dataContext) : IBaseRepository<TEntity, TKey>
    where TEntity : class
{
    public virtual async Task<List<TEntity>> GetAllAsync()
        => await dataContext.Set<TEntity>().AsNoTracking().ToListAsync();

    public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        => await dataContext.Set<TEntity>().FindAsync([id]);

    public virtual async Task CreateAsync(TEntity entity)
    {
        await dataContext.Set<TEntity>().AddAsync(entity);
        await dataContext.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        dataContext.Set<TEntity>().Update(entity);
        await dataContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        dataContext.Set<TEntity>().Remove(entity);
        await dataContext.SaveChangesAsync();
    }

    public void Dispose()
        => dataContext.Dispose();
}