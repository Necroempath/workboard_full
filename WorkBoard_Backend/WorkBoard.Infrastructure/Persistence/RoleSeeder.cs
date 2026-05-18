using Microsoft.EntityFrameworkCore;
using WorkBoard.Domain;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence;

public static class RoleSeeder
{
    public static async Task SeedAsync(WorkBoardDbContext context)
    {
        IEnumerable<Role> roles = new List<Role>()
        {
            new ("User"),
            new ("Manager"),
            new ("Admin")
        };

        var rolesInDb = await context.Roles.Select(r => r.Name).ToListAsync();

        var rolesToAdd = roles
            .Where(r => !rolesInDb.Any(x => x.Equals(r.Name, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        context.Roles.AddRange(rolesToAdd);

        await context.SaveChangesAsync();

        if (!context.Users.Any(u => u.Roles.Any(r => r.Role.Name == "Admin")))
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Aa123!");

            var admin = new User("Admin", "admin@mail.com", passwordHash);

            var adminRole = await context.Roles.FirstAsync(r => r.Name == "Admin");

            admin.AssignRole(adminRole);

            context.Users.Add(admin);

            await context.SaveChangesAsync();
        }
    }
}
