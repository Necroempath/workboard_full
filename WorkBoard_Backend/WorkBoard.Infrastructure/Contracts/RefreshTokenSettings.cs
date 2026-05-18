namespace WorkBoard.Infrastructure.Contracts;

public sealed class RefreshTokenSettings
{
    public const string SectionName = "RefreshTokenSettings";
    public int ExpirationDays { get; init; }
}