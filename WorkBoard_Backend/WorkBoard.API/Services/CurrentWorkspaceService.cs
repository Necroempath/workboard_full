using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Entities;

namespace WorkBoard.API.Services;

public sealed class CurrentWorkspaceService : ICurrentWorkspaceService
{
    public Guid WorkspaceId { get; set; }
    public WorkspaceMembership Membership { get; set; } = null!;
}