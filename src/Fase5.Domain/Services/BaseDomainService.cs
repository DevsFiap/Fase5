using Fase5.Domain.Core;

namespace Fase5.Domain.Services;

public abstract class BaseDomainService<TEntity, TKey>(IBaseRepository<TEntity, TKey> _baseRepository) 
    : IBaseDomainService<TEntity, TKey>
    where TEntity : class
{
    public virtual async Task<List<TEntity>> GetAllAsync()
        => await _baseRepository.GetAllAsync();

    public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        => await _baseRepository.GetByIdAsync(id);

    public virtual async Task AddAsync(TEntity entity)
        => await _baseRepository.CreateAsync(entity);

    public virtual async Task ModifyAsync(TEntity entity)
        => await _baseRepository.UpdateAsync(entity);

    public virtual async Task RemoveAsync(TEntity entity)
        => await _baseRepository.DeleteAsync(entity);

    public void Dispose()
        => _baseRepository.Dispose();
}