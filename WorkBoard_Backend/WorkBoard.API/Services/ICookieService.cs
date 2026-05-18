using Azure.Core;

namespace WorkBoard.API.Services;

public interface ICookieService
{
    void SetRefreshToken(HttpResponse response, string token);
    void DeleteRefreshToken(HttpResponse response);
    string? TryGet(HttpRequest request);
}
