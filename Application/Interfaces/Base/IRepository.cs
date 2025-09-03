using Domain.Abstractions;

namespace Application.Interfaces.Base;

public interface IRepository<TEntity, TId> where TEntity : Entity<TId>
{
    Task<TEntity?>             GetByIdAsync(TId id, bool asTracking = false);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task                       CreateAsync(TEntity entity);
    Task                       UpdateAsync(TEntity entity);
    Task                       DeleteAsync(TId id);
    IQueryable<TEntity>        Query();
}