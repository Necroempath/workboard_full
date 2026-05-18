using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IPasswordResetTokenRepository
{
    Task<PasswordResetToken?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<ICollection<PasswordResetToken>> GetByUserIdAsync(Guid userId, CancellationToken ct);
    Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken ct);
    Task<PasswordResetToken> AddTokenAsync(PasswordResetToken token, CancellationToken ct);
    Task<bool> DeleteTokenAsync(Guid id, CancellationToken ct);
    Task Save(CancellationToken ct);

}
