using Microsoft.Extensions.Options;
using WorkBoard.Infrastructure.Contracts;

namespace WorkBoard.API.Services;

public class CookieService : ICookieService
{
    private readonly RefreshTokenSettings _settings;

    public CookieService(IOptions<RefreshTokenSettings> options)
    {
        _settings = options.Value;
    }

    public void SetRefreshToken(HttpResponse response, string token)
    {
        response.Cookies.Append("refreshToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddDays(_settings.ExpirationDays)
        });
    }

    public void DeleteRefreshToken(HttpResponse response)
    {
        response.Cookies.Delete("refreshToken", new CookieOptions
        {
            Path = "/"
        });
    }

    public string? TryGet(HttpRequest request)
    {
        if (!request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            return null;

        return refreshToken;
    }
}
