using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}