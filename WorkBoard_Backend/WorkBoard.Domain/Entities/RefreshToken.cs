namespace WorkBoard.Domain.Entities;

public sealed class RefreshToken
{
    public Guid Id { get; }
    public string TokenHash { get; private set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? RevokedAt { get; private set; }
    public Guid? ReplacedByTokenId { get; private set; }
    public Guid UserId { get; }
    public User User { get; } = null!;

    private RefreshToken() { }

    public RefreshToken(string tokenHash, Guid userId, DateTimeOffset expiresAt)
    {
        if (string.IsNullOrWhiteSpace(tokenHash))
            throw new ArgumentException("Token can not be empty");

        if (userId == Guid.Empty)
            throw new ArgumentException("User Id can not be empty");

        if (expiresAt <= DateTimeOffset.UtcNow)
            throw new ArgumentException("Expire date must be in the future");

        TokenHash = tokenHash;
        UserId = userId;
        ExpiresAt = expiresAt;
    }

    public void Revoke()
    {
        RevokedAt = DateTimeOffset.UtcNow;
    }

    public void SetReplaceToken(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Token Id cannot be empty");

        ReplacedByTokenId = id;
    }
}
