using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Extensions;
using WorkBoard.Domain.Enums;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Commands;
public sealed class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, WorkspaceMembershipResponseDto>
{
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public AddMemberCommandHandler(IWorkspaceMembershipRepository membershipRepository, IUserRepository userRepository,
        ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<WorkspaceMembershipResponseDto> Handle(AddMemberCommand command, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanInviteMembers())
            throw new InvalidOperationException("Admins or Owner only allowed for this action");

        if (command.Request.Role == WorkspaceRole.Owner)
            throw new InvalidOperationException("Only one Owner allowed per Workspace");

        var user = await _userRepository.GetByEmailAsync(command.Request.Email, ct);

        if (user is null)
            throw new InvalidOperationException("USER_NOT_FOUND");

        var membership = await _membershipRepository.GetMembershipAsync(user.Id, _currentWorkspace.WorkspaceId, ct);

        if (membership is not null)
            throw new InvalidOperationException($"ALREADY_MEMBER");

        WorkspaceMembership newMembership = new(user.Id, _currentWorkspace.WorkspaceId, command.Request.Role, DateTimeOffset.UtcNow);
        
        await _membershipRepository.AddMembershipAsync(newMembership, ct);

        return _mapper.Map<WorkspaceMembershipResponseDto>(newMembership);
    }
}