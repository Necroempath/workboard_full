using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Issues.Queries;

public sealed record GetAllIssuesQuery(Guid ProjectId) : IRequest<IEnumerable<IssueResponseDto>>, IProjectRequest;