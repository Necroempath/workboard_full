using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using WorkBoard.Application.Abstractions;
using WorkBoard.Infrastructure.Contracts;

namespace WorkBoard.Infrastructure.Implementations;

public sealed class PasswordResetTokenGenerator : IPasswordResetTokenGenerator
{
    private readonly PasswordResetTokenSettings _settings;

    public PasswordResetTokenGenerator(IOptions<PasswordResetTokenSettings> settings)
    {
        _settings = settings.Value;
    }

    public (string, string, DateTimeOffset) Generate()
    {
        var token = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

        var hash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(token)));

        var expiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes);

        return (token, hash, expiresAt);
    }
}

