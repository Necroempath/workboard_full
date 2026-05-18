using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public sealed class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, WorkspaceRole>
{
    private readonly IWorkspaceMembershipRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetRoleQueryHandler(IWorkspaceMembershipRepository repository, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<WorkspaceRole> Handle(GetRoleQuery request, CancellationToken ct)
    {
        var membership = await _repository.GetMembershipAsync(_currentUser.UserId, request.WorkspaceId, ct);

        return membership.Role;
    }
}