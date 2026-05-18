using WorkBoard.Domain.Enums;

namespace WorkBoard.Domain.Entities;

public sealed class WorkspaceMembership
{
    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public User Member { get; private set; } = null!;
    public Guid WorkspaceId { get; private set; }
    public Workspace Workspace { get; private set; } = null!;

    public WorkspaceRole Role { get; set; }
    public DateTimeOffset JoinedAt { get; private set; }

    private WorkspaceMembership() { }

    public WorkspaceMembership(Guid memberId, Guid workspaceId, WorkspaceRole role, DateTimeOffset joinedAt)
    {
        MemberId = memberId;
        WorkspaceId = workspaceId;
        Role = role;
        JoinedAt = joinedAt;
    }
}
