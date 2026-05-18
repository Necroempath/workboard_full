namespace WorkBoard.Infrastructure.Contracts;

public sealed class EmailSettings
{
    public const string SectionName = "EmailSettings";
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string SenderEmail { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
