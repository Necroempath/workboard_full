using FluentValidation.AspNetCore;
using FluentValidation;
using WorkBoard.Application;

namespace WorkBoard.API.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationMarker).Assembly);

        services.AddFluentValidationAutoValidation();

        return services;
    }
}
