using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Abstractions;

public interface ICurrentWorkspaceService
{
    Guid WorkspaceId { get; set; }
    WorkspaceMembership Membership { get; set; }
}
