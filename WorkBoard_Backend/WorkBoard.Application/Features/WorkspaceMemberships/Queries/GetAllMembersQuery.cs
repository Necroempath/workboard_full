using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Queries;

public sealed record GetAllMembersQuery(Guid WorkspaceId) : IRequest<WorkspaceMembershipResponseDto>, IWorkspaceRequest;
