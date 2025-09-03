using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
        

        builder
            .HasMany<OrderItem>("_orderItems")
            .WithOne()
            .HasForeignKey(x => x.FatherId);
    }
}