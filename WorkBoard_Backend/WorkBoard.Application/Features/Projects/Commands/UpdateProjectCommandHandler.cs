using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Projects.Commands;

public sealed class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectResponseDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<ProjectResponseDto> Handle(UpdateProjectCommand request, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can manage projects");

        var project = await _projectRepository.GetByIdAsync(request.ProjectId, ct)
            ?? throw new InvalidOperationException("Invalid Project Id");

        project.Rename(request.Name);
        await _projectRepository.SaveAsync(ct);

        return _mapper.Map<ProjectResponseDto>(project);
    }
}