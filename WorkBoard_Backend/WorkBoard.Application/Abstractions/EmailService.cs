namespace WorkBoard.Application.Abstractions;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string toEmail, string userName, string resetLink);
}
