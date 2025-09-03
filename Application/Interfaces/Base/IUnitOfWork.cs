using Domain.Abstractions;

namespace Application.Interfaces.Base;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity, TId> Repository<TEntity, TId>()
        where TEntity : Entity<TId>
        where TId : notnull;

    Task<int> SaveChangesAsync();
    Task      BeginTransactionAsync();
    Task      CommitTransactionAsync();
    Task      RollbackTransactionAsync();
}