using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Order> Orders { get; }
    DbSet<OrderItem> OrderItem { get; }
    DbSet<Domain.Entities.PurchasePlan> PurchasePlan { get; }
    DbSet<Domain.Entities.PurchasePlanLine> PurchasePlanLine { get; }

    DbSet<Domain.Entities.ApprovalLevel> ApprovalLevel { get; }
    DbSet<ApprovalLevelUserAppr> ApprovalLevelUserApprs { get; }
    DbSet<ApprovalTemplate> ApprovalTemplates { get; }
    DbSet<ApprovalTemplateCondition> ApprovalTemplateConditions { get; }
    DbSet<ApprovalTemplateCreator> ApprovalTemplateCreators { get; }
    DbSet<ApprovalTemplateDocument> ApprovalTemplateDocuments { get; }
    DbSet<ApprovalTemplateProcess> ApprovalTemplateProcesses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}