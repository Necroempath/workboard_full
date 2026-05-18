using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IWorkspaceMembershipRepository
{
    Task<IEnumerable<WorkspaceMembership>> GetMembersAsync(Guid workspaceId, CancellationToken ct);
    Task<IEnumerable<WorkspaceMembership>> GetWorkspacesAsync(Guid userId, CancellationToken ct);
    Task<WorkspaceMembership?> GetMembershipAsync(Guid userId, Guid workspaceId, CancellationToken ct);
    Task<WorkspaceMembership> AddMembershipAsync(WorkspaceMembership membership, CancellationToken ct); 
    Task<bool> RemoveMemberAsync(Guid memberId, Guid workspaceId, CancellationToken ct);
    Task<WorkspaceMembership?> ChangeRoleAsync(Guid memberId, Guid workspaceId, WorkspaceRole role, CancellationToken ct);
}
