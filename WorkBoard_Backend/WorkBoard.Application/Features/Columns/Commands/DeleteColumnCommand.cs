using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Columns.Commands;

public sealed record DeleteColumnCommand(Guid ColumnId) : IRequest<bool>, IColumnRequest;