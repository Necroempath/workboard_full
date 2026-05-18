using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Queries;

public sealed class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery,  WorkspaceMembershipResponseDto>
{
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public GetAllMembersQueryHandler(IWorkspaceMembershipRepository membershipRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<WorkspaceMembershipResponseDto> Handle(GetAllMembersQuery request, CancellationToken ct)
    {
        var members = await _membershipRepository.GetMembersAsync(_currentWorkspace.WorkspaceId, ct);

        WorkspaceMembershipResponseDto dto = new() { CurrentUserRole = _currentWorkspace.Membership.Role };

        List<MembershipDto> dtos = new();

        foreach (var member in members)
        {
            dtos.Add(new() { UserId = member.MemberId, Name = member.Member.Name, Email = member.Member.Email, Role = member.Role });
        }

        dto.Members = [.. dtos];

        return dto;
    }
}