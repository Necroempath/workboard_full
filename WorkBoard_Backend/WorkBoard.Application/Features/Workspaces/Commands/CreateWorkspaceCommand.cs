using MediatR;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed record CreateWorkspaceCommand(CreateWorkspaceRequest Request) : IRequest<WorkspaceResponseDto>;
