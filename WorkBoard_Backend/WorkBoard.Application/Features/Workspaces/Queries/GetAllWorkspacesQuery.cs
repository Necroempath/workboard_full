using MediatR;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public sealed record GetAllWorkspacesQuery : IRequest<IEnumerable<WorkspaceResponseDto>>;