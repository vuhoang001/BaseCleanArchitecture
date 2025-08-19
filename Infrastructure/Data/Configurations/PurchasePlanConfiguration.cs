using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PurchasePlanConfiguration : IEntityTypeConfiguration<PurchasePlan>
{
    public void Configure(EntityTypeBuilder<PurchasePlan> builder)
    {
        builder
            .HasMany(x => x.ItemLines)
            .WithOne()
            .HasForeignKey(x => x.FatherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}