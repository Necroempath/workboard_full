using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfWorkspaceRepository : IWorkspaceRepository
{
    private readonly WorkBoardDbContext _context;

    public EfWorkspaceRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Workspace> AddAsync(Workspace workspace, CancellationToken token)
    {
        await _context.Workspaces.AddAsync(workspace, token);
        await _context.SaveChangesAsync(token);

        return workspace;
    }

    public async Task<IEnumerable<Workspace>> GetAllAsync(Guid userId, CancellationToken token)
    {
        return await _context.Workspaces.Where(w => w.Members.Any(wm => wm.MemberId == userId))
            .Include(w => w.Members
            .Where(m => m.MemberId == userId))
            .ToListAsync(token);
    }

    public async Task<Workspace?> GetByIdAsync(Guid id, Guid userId, CancellationToken token)
    {
        return await _context.Workspaces
            .FirstOrDefaultAsync(w => w.Id == id && w.Members.Any(wm => wm.MemberId == userId), token);
    }
}