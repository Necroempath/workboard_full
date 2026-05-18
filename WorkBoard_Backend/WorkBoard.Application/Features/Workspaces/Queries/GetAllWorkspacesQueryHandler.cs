using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public sealed class GetAllWorkspacesQueryHandler : IRequestHandler<GetAllWorkspacesQuery, IEnumerable<WorkspaceResponseDto>>
{
    private readonly IWorkspaceRepository _repository;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public GetAllWorkspacesQueryHandler(IWorkspaceRepository repository, ICurrentUserService currentUser, IMapper mapper)
    {
        _repository = repository;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkspaceResponseDto>> Handle(GetAllWorkspacesQuery query, CancellationToken token)
    {
        var workspaces = await _repository.GetAllAsync(_currentUser.UserId, token);

        return _mapper.Map<IEnumerable<WorkspaceResponseDto>>(workspaces);
    }
}
