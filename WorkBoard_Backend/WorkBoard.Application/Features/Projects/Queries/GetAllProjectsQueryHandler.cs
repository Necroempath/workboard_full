using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;

namespace WorkBoard.Application.Features.Projects.Queries;

public sealed class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectResponseDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public GetAllProjectsQueryHandler(IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectResponseDto>> Handle(GetAllProjectsQuery request, CancellationToken ct)
    {
        var projects = await _projectRepository.GetByWorkspaceIdAsync(_currentWorkspace.WorkspaceId, ct);

        return _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);
    }
}