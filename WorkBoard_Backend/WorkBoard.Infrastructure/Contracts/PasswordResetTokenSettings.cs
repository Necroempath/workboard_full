namespace WorkBoard.Infrastructure.Contracts;

public sealed class PasswordResetTokenSettings
{
    public const string SectionName = "PasswordResetTokenSettings";
    public int ExpiryMinutes { get; init; }
}
