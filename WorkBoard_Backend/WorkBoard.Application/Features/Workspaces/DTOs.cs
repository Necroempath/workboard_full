using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Features.Workspaces;

public sealed class WorkspaceResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public WorkspaceRole Role { get; set; }
}
public sealed class CreateWorkspaceRequest
{
    public string Name { get; set; } = string.Empty;
}
public sealed class UpdateWorkspaceRequest
{
    public string Name { get; set; } = string.Empty;
}