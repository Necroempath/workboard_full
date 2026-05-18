using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfRoleRepository : IRoleRepository
{
    private readonly WorkBoardDbContext _context;

    public EfRoleRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Role> AddAsync(Role role, CancellationToken token)
    {
        await _context.Roles.AddAsync(role, token);
        await _context.SaveChangesAsync(token);

        return role;
    }

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id, token);
    }

    public async Task<Role?> GetByNameAsync(string roleName, CancellationToken token)
    {
        return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, token);
    }
}