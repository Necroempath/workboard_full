using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Projects.Queries;

public sealed record GetAllProjectsQuery(Guid WorkspaceId) : IRequest<IEnumerable<ProjectResponseDto>>, IWorkspaceRequest;