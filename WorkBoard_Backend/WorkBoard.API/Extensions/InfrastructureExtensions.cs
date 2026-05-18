using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions;
using WorkBoard.Infrastructure.Persistence;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Infrastructure.Persistence.Repositories;
using WorkBoard.Infrastructure.Implementations;

namespace WorkBoard.API.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkBoardDbContext>(options =>
        {
            Console.WriteLine(configuration.GetConnectionString("Default"));
            options.UseSqlServer(configuration.GetConnectionString("Default"), sql =>
            {
                sql.EnableRetryOnFailure();
            });
        });

        services.AddScoped<IWorkspaceRepository, EfWorkspaceRepository>();
        services.AddScoped<IProjectRepository, EfProjectRepository>();
        services.AddScoped<IIssueRepository, EfIssueRepository>();
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IRoleRepository, EfRoleRepository>();
        services.AddScoped<IWorkspaceMembershipRepository, EfWorkspaceMembershipRepository>();
        services.AddScoped<IRefreshTokenRepository, EfRefreshTokenRepository>();
        services.AddScoped<IPasswordResetTokenRepository, EfPasswordResetTokenRepository>();

        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddScoped<IPasswordResetTokenGenerator, PasswordResetTokenGenerator>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}