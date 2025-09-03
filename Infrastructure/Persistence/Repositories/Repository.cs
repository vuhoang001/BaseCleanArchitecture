using Application.Interfaces.Base;
using Infrastructure.Data;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity>       _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet   = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id, bool asTracking = false)
    {
        if (asTracking) return await _dbSet.FindAsync(id);
        return await _dbSet.FirstOrDefaultAsync(x => Equals(x.Id, id));
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null) _dbSet.Remove(entity);
    }

    public IQueryable<TEntity> Query()
    {
        return _dbSet.AsNoTracking();
    }
}