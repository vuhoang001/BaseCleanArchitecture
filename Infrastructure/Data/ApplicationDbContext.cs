using System.Reflection;
using Application.Data;
using Infrastructure.Idenitty;
using Infrastructure.Idenitty.Mapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, string,
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}