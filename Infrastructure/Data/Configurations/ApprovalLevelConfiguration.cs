using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ApprovalLevelConfiguration : IEntityTypeConfiguration<ApprovalLevel>,
    IEntityTypeConfiguration<ApprovalLevelUserAppr>
{
    public void Configure(EntityTypeBuilder<ApprovalLevel> builder)
    {
        builder
            .HasMany(x => x.ApprovalLevelUserApprs)
            .WithOne()
            .HasForeignKey(x => x.FatherId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<ApprovalLevelUserAppr> builder)
    {
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}