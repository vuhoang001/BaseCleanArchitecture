using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Order> Orders { get; }
    DbSet<OrderItem> OrderItem { get; }
    DbSet<ApprovalLevel> ApprovalLevel { get; }

    DbSet<OrgLevel> OrgLevel { get; }
    DbSet<Domain.Entities.PurchaseRequest> PurchaseRequest { get; }
    DbSet<PurchaseRequestItems> PurchaseRequestItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}