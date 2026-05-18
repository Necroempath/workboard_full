using MediatR;
using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public sealed record GetRoleQuery(Guid WorkspaceId) : IRequest<WorkspaceRole>;
