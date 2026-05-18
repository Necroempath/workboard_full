using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Projects.Commands;

public sealed record UpdateProjectCommand(string Name, Guid ProjectId) : IRequest<ProjectResponseDto>, IProjectRequest;
