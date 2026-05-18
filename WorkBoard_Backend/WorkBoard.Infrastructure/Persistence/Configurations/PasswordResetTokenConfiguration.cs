using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Configurations;

class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
{

    public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
    {
        builder.ToTable("PasswordResetTokens");

        builder.HasKey(x => x.Id);

        builder.HasOne(rt => rt.User)
               .WithMany(u => u.PasswordResetTokens)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
