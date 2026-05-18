using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, WorkspaceResponseDto>
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public CreateWorkspaceCommandHandler(IWorkspaceRepository workspaceRepository, IWorkspaceMembershipRepository membershipRepository, 
        IMapper mapper, ICurrentUserService currentUser)
    {
        _workspaceRepository = workspaceRepository;
        _membershipRepository = membershipRepository;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<WorkspaceResponseDto> Handle(CreateWorkspaceCommand command, CancellationToken token)
    {
        var workspace = _mapper.Map<Workspace>(command.Request);
        
        if (!_currentUser.IsAuthenticated)
            throw new UnauthorizedAccessException("User not authenticated");

        workspace.SetOwner(_currentUser.UserId);

        await _workspaceRepository.AddAsync(workspace, token);

        WorkspaceMembership membership = new(workspace.OwnerId, workspace.Id, WorkspaceRole.Owner, DateTimeOffset.UtcNow);

        await _membershipRepository.AddMembershipAsync(membership, token);

        return _mapper.Map<WorkspaceResponseDto>(workspace);
    }
}
