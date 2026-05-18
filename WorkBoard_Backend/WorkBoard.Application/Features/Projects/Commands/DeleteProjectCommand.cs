using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Projects.Commands;

public sealed record DeleteProjectCommand(Guid ProjectId) : IRequest<bool>, IProjectRequest;
