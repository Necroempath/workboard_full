using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfUserRepository : IUserRepository
{
    private readonly WorkBoardDbContext _context;

    public EfUserRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user, CancellationToken token)
    {
        await _context.Users.AddAsync(user, token);
        await _context.SaveChangesAsync(token);

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken token)
    {
        return await _context.Users
            .Include(u => u.Roles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email, token);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await _context.Users
            .Include(u => u.Roles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, token);
    }

    public async Task SaveAsync(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);
    }

}