using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseRequest>
{
    public void Configure(EntityTypeBuilder<PurchaseRequest> builder)
    {
        builder
            .HasMany(x => x.PurchaseRequestItems)
            .WithOne()
            .HasForeignKey(x => x.FatherId);
    }
}