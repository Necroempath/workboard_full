using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed record DeleteIssueCommand(Guid IssueId) : IRequest<bool>, IIssueRequest;
