using WorkBoard.Infrastructure.Persistence;

namespace WorkBoard.API.Extensions;

public static class SeederExtensions
{
    public static async Task EnsureRoleSeededAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<WorkBoardDbContext>()!;

        await RoleSeeder.SeedAsync(dbContext);
    }
}
