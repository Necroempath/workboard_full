using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed record MoveIssueCommand(MoveIssueRequest Request, Guid IssueId) : IRequest<IssueResponseDto>, IIssueRequest;

