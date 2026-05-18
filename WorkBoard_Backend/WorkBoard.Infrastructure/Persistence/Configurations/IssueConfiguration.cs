using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Configurations;

public sealed class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.ToTable("Issues");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired(false);

        builder.Property(x => x.ColumnId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.HasOne(x => x.Column)
            .WithMany(x => x.Issues)
            .HasForeignKey(x => x.ColumnId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Issues)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}