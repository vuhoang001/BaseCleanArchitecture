using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ApprovalTemplateConfiguration : IEntityTypeConfiguration<ApprovalTemplate>,
    IEntityTypeConfiguration<ApprovalTemplateCreator>, IEntityTypeConfiguration<ApprovalTemplateProcess>
{
    public void Configure(EntityTypeBuilder<ApprovalTemplate> builder)
    {
        builder.HasMany(x => x.ApprovalTemplateConditions)
            .WithOne()
            .HasForeignKey(x => x.FatherId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.ApprovalTemplateCreators)
            .WithOne()
            .HasForeignKey(x => x.FatherId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.ApprovalTemplateDocuments)
            .WithOne()
            .HasForeignKey(x => x.FatherId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.ApprovalTemplateProcesses)
            .WithOne()
            .HasForeignKey(x => x.FatherId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<ApprovalTemplateCreator> builder)
    {
        builder
            .HasOne(x => x.Creator)
            .WithMany()
            .HasForeignKey(x => x.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public void Configure(EntityTypeBuilder<ApprovalTemplateProcess> builder)
    {
        builder
            .HasOne(x => x.ApprovalLevel)
            .WithMany()
            .HasForeignKey(x => x.ApporvalLevelId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}