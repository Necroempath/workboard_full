using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Commands;

public sealed record RemoveMemberCommand(Guid MemberId, Guid WorkspaceId) : IRequest<bool>, IWorkspaceRequest;