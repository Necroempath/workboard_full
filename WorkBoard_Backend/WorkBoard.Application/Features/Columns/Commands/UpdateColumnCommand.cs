using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Columns.Commands;

public sealed record UpdateColumnCommand(string Name, Guid ColumnId) : IRequest<ColumnResponseDto>, IColumnRequest;
