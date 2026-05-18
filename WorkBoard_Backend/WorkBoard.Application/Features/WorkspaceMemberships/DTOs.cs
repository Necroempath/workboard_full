using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Features.WorkspaceMemberships;

public sealed class WorkspaceMembershipResponseDto
{
    public WorkspaceRole CurrentUserRole { get; set; }
    public MembershipDto[] Members { get; set; } = null!;
}
public sealed class MembershipDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public WorkspaceRole Role { get; set; }
}

public sealed class AddMemberRequest
{
    public string Email { get; set; } = string.Empty;
    public WorkspaceRole Role { get; set; }
}

public sealed class ChangeRoleRequest
{
    public Guid UserId { get; set; }
    public WorkspaceRole Role { get; set; }
}