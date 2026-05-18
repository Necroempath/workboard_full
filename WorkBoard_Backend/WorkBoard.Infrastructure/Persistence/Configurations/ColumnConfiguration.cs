using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Configurations;

public sealed class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.ToTable("Columns");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ProjectId)
            .IsRequired();

        builder.Navigation(x => x.Issues)
          .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.HasOne(x => x.Project)
            .WithMany(x => x.Columns)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}