using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Enums;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfWorkspaceMembershipRepository : IWorkspaceMembershipRepository
{
    private readonly WorkBoardDbContext _context;

    public EfWorkspaceMembershipRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<WorkspaceMembership> AddMembershipAsync(WorkspaceMembership membership, CancellationToken ct)
    {
        await _context.WorkspaceMemberships.AddAsync(membership, ct);

        await _context.SaveChangesAsync(ct);

        return membership;
    }

    public async Task<WorkspaceMembership?> ChangeRoleAsync(Guid memberId, Guid workspaceId, WorkspaceRole role, CancellationToken ct)
    {
        var membership = await _context.WorkspaceMemberships
            .FirstOrDefaultAsync(wm => wm.MemberId == memberId && wm.WorkspaceId == workspaceId, ct);

        if (membership is null)
            return null;

        membership.Role = role;

        await _context.SaveChangesAsync(ct);

        return membership;
    }

    public async Task<IEnumerable<WorkspaceMembership>> GetMembersAsync(Guid workspaceId, CancellationToken ct)
    {
        return await _context.WorkspaceMemberships.Where(wm => wm.WorkspaceId == workspaceId)
                                                  .Include(wm => wm.Member)
                                                  .Include(wm => wm.Workspace).ToListAsync(ct);
    }

    public async Task<WorkspaceMembership?> GetMembershipAsync(Guid userId, Guid workspaceId, CancellationToken ct)
    {
        return await _context.WorkspaceMemberships
            .Include(wm => wm.Workspace)
            .Include(wm => wm.Member)
            .FirstOrDefaultAsync(wm => wm.MemberId == userId && wm.WorkspaceId == workspaceId, ct);
    }

    public async Task<IEnumerable<WorkspaceMembership>> GetWorkspacesAsync(Guid userId, CancellationToken ct)
    {
        return await _context.WorkspaceMemberships
            .Where(wm => wm.MemberId == userId).ToListAsync(ct);

    }

    public async Task<bool> RemoveMemberAsync(Guid memberId, Guid workspaceId, CancellationToken ct)
    {
        var membership = await GetMembershipAsync(memberId, workspaceId, ct);

        if (membership is null)
            return false;

        _context.WorkspaceMemberships.Remove(membership);

        await _context.SaveChangesAsync(ct);

        return true;
    }
}