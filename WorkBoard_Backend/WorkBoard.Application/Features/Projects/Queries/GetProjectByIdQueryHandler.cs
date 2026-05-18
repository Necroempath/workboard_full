using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;

namespace WorkBoard.Application.Features.Projects.Queries;

public sealed class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectResponseDto?>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<ProjectResponseDto?> Handle(GetProjectByIdQuery request, CancellationToken ct)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, ct);

        if (project is null)
            return null;

        if (project.WorkspaceId != _currentWorkspace.WorkspaceId)
            throw new InvalidOperationException("Project is out of current context");

        return _mapper.Map<ProjectResponseDto>(project);
    }
}
