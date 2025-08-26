using System.Reflection;
using Application.Data;
using Infrastructure.Idenitty;
using Infrastructure.Idenitty.Mapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, int,
        ApplicationUserClaims, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItem => Set<OrderItem>();

    public DbSet<OrgLevel> OrgLevel => Set<OrgLevel>();
    public DbSet<PurchasePlan> PurchasePlan => Set<PurchasePlan>();
    public DbSet<PurchasePlanLine> PurchasePlanLine => Set<PurchasePlanLine>();
    public DbSet<ApprovalLevel> ApprovalLevel => Set<ApprovalLevel>();
    public DbSet<ApprovalLevelUserAppr> ApprovalLevelUserApprs => Set<ApprovalLevelUserAppr>();
    public DbSet<ApprovalTemplate> ApprovalTemplates => Set<ApprovalTemplate>();
    public DbSet<ApprovalTemplateCondition> ApprovalTemplateConditions => Set<ApprovalTemplateCondition>();
    public DbSet<ApprovalTemplateCreator> ApprovalTemplateCreators => Set<ApprovalTemplateCreator>();
    public DbSet<ApprovalTemplateDocument> ApprovalTemplateDocuments => Set<ApprovalTemplateDocument>();
    public DbSet<ApprovalTemplateProcess> ApprovalTemplateProcesses => Set<ApprovalTemplateProcess>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}