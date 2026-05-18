using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using WorkBoard.Application.Abstractions;
using WorkBoard.Infrastructure.Contracts;

namespace WorkBoard.Infrastructure.Implementations;

public sealed class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendPasswordResetEmailAsync(string toEmail, string userName, string resetLink)
    {
        var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(
                _settings.SenderEmail,
                _settings.Password
            ),
            EnableSsl = true
        };

        var mail = new MailMessage
        {
            From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
            Subject = "Reset your password",
            Body = BuildHtmlBody(userName, resetLink),
            IsBodyHtml = true
        };

        mail.To.Add(toEmail);

        await client.SendMailAsync(mail);
    }

    private string BuildHtmlBody(string userName, string resetLink)
    {
        return $@"
        <div style='font-family: Arial; max-width: 600px; margin: auto;'>
            <h2>Password Reset</h2>
            <p>Hello {userName},</p>
            <p>You requested to reset your password.</p>

            <a href='{resetLink}'
               style='display:inline-block;padding:10px 20px;
               background:#3b82f6;color:white;border-radius:6px;
               text-decoration:none;margin-top:10px;'>
               Reset Password
            </a>

            <p style='margin-top:20px; font-size:12px; color:gray;'>
                This link will expire in 15 minutes.
                If you didn’t request this, ignore this email.
            </p>
        </div>";
    }
}
