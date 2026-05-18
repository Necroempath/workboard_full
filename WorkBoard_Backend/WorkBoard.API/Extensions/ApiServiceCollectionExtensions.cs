using WorkBoard.API.Services;
using WorkBoard.Application.Abstractions;

namespace WorkBoard.API.Extensions;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ICurrentWorkspaceService, CurrentWorkspaceService>();
        services.AddScoped<ICookieService, CookieService>();

        return services;
    }
}
