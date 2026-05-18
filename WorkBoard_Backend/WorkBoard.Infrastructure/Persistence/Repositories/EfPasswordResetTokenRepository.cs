using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfPasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly WorkBoardDbContext _context;

    public EfPasswordResetTokenRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<PasswordResetToken?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.PasswordResetTokens.FirstOrDefaultAsync(rt => rt.Id == id, ct);
    }

    public async Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken ct)
    {
        var hash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(token)));

        return await _context.PasswordResetTokens.FirstOrDefaultAsync(rt => rt.TokenHash == hash, ct);
    }

    public async Task<PasswordResetToken> AddTokenAsync(PasswordResetToken token, CancellationToken ct)
    {
        await _context.PasswordResetTokens.AddAsync(token, ct);

        await _context.SaveChangesAsync(ct);

        return token;
    }

    public async Task<bool> DeleteTokenAsync(Guid id, CancellationToken ct)
    {
        var passwordResetTokens = await _context.PasswordResetTokens.FirstOrDefaultAsync(rt => rt.Id == id, ct);

        if (passwordResetTokens is null)
            return false;

        _context.PasswordResetTokens.Remove(passwordResetTokens);

        await _context.SaveChangesAsync(ct);

        return true;
    }

    public async Task Save(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
    }

    public async Task<ICollection<PasswordResetToken>> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await _context.PasswordResetTokens
            .Where(prt => prt.UserId == userId && prt.IsUsed == false)
            .ToListAsync(ct);
    }
}
