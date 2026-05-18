using MediatR;
using WorkBoard.Application;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.API.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly));

        services.AddAutoMapper(typeof(ApplicationMarker).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(WorkspaceMembershipBehavior<,>));

        return services;
    }
}
