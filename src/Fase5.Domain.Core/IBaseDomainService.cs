namespace Fase5.Domain.Core;

public interface IBaseDomainService<TEntity, TKey> : IDisposable
    where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task ModifyAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}