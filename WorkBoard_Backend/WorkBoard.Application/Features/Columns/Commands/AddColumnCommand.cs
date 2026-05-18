using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Columns.Commands;

public sealed record AddColumnCommand(string Name, Guid ProjectId) : IRequest<ColumnResponseDto>, IProjectRequest;
