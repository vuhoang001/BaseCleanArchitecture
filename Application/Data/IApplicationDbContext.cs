using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Order> Orders { get; }
    DbSet<OrderItem> OrderItem { get; }
    DbSet<Domain.Entities.PurchasePlan> PurchasePlan { get; }
    DbSet<Domain.Entities.PurchasePlanLine> PurchasePlanLine { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}