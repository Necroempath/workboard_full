using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using WorkBoard.Application.Abstractions;
using WorkBoard.Infrastructure.Contracts;

namespace WorkBoard.Infrastructure.Implementations;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private readonly RefreshTokenSettings _settings;

    public RefreshTokenGenerator(IOptions<RefreshTokenSettings> settings)
    {
        _settings = settings.Value;
    }

    public (string, string, DateTimeOffset) Generate()
    {
        var token = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

        var hash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(token)));

        var expiresAt = DateTime.UtcNow.AddDays(_settings.ExpirationDays);

        return (token, hash, expiresAt);
    }    
}
