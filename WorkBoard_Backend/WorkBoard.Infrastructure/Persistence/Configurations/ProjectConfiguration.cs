using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.WorkspaceId)
            .IsRequired();

        builder.Navigation(x => x.Issues)
        .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Columns)
         .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.HasOne(x => x.Workspace)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}