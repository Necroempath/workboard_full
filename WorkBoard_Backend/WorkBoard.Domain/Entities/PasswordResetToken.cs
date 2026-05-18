namespace WorkBoard.Domain.Entities;

public sealed class PasswordResetToken
{
    public Guid Id { get; }
    public string TokenHash { get; private set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; private set; }
    public bool IsUsed { get; set; }
    public Guid UserId { get; }
    public User User { get; set; } = null!;

    public PasswordResetToken(string tokenHash, DateTimeOffset expiresAt, Guid userId)
    {
        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
        UserId = userId;
    }
}
