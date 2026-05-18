using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Projects.Commands;

public sealed record CreateProjectCommand(CreateProjectRequest Request, Guid WorkspaceId) : IRequest<ProjectResponseDto>, IWorkspaceRequest;
