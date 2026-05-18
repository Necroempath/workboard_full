using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Commands;

public sealed class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, WorkspaceMembershipResponseDto>
{
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public ChangeRoleCommandHandler(IWorkspaceMembershipRepository membershipRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<WorkspaceMembershipResponseDto> Handle(ChangeRoleCommand command, CancellationToken ct)
    {
        var userToManage = await _membershipRepository.GetMembershipAsync(command.Request.UserId, _currentWorkspace.WorkspaceId, ct) ??
                    throw new InvalidOperationException("USER_NOT_FOUND");

        if (!_currentWorkspace.Membership.Role.CanAssignRole(userToManage.Role))
            throw new InvalidOperationException("NO_PERMISSION");

        var membership = await _membershipRepository.ChangeRoleAsync(command.Request.UserId, _currentWorkspace.WorkspaceId, command.Request.Role, ct);

        return _mapper.Map<WorkspaceMembershipResponseDto>(membership);
    }
}