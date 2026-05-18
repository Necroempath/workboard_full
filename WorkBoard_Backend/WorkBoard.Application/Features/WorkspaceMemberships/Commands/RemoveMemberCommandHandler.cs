using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Commands;

public sealed class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, bool>
{
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;

    public RemoveMemberCommandHandler(IWorkspaceMembershipRepository membershipRepository, ICurrentWorkspaceService currentWorkspace)
    {
        _membershipRepository = membershipRepository;
        _currentWorkspace = currentWorkspace;
    }

    public async Task<bool> Handle(RemoveMemberCommand command, CancellationToken ct)
    {
        var userToRemove = await _membershipRepository.GetMembershipAsync(command.MemberId, _currentWorkspace.WorkspaceId, ct) ??
            throw new InvalidOperationException("USER_NOT_FOUND");

        if (!_currentWorkspace.Membership.Role.CanDeleteMembers(userToRemove.Role))
            throw new InvalidOperationException("NO_PERMISSION");

        if (_currentWorkspace.Membership.MemberId == command.MemberId)
            throw new InvalidOperationException("SELF_DESTRUCTION");

        var isDeleted = await _membershipRepository.RemoveMemberAsync(command.MemberId, _currentWorkspace.WorkspaceId, ct);

        if (!isDeleted)
            throw new InvalidOperationException("USER_NOT_FOUND");

        return isDeleted;
    }
}
