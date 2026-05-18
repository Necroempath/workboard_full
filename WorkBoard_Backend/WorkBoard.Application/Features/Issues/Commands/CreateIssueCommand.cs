using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed record CreateIssueCommand(CreateIssueRequest Request, Guid ColumnId) : IRequest<IssueResponseDto>, IColumnRequest;