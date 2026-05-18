using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Commands;

public sealed record AddMemberCommand(AddMemberRequest Request, Guid WorkspaceId) : IRequest<WorkspaceMembershipResponseDto>, IWorkspaceRequest;
