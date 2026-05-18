using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed record UpdateIssueCommand(UpdateIssueRequest Request, Guid IssueId) : IRequest<IssueResponseDto>, IIssueRequest;

