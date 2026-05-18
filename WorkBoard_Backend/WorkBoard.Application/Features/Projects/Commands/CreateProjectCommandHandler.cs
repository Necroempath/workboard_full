using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Projects.Commands;

public sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectResponseDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<ProjectResponseDto> Handle(CreateProjectCommand command, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can create projects");

        Project project = new(command.Request.Name, command.WorkspaceId);

        await _projectRepository.CreateAsync(project, ct);

        project.InitializeDefaultColumns();

        await _projectRepository.SaveAsync(ct);

        return _mapper.Map<ProjectResponseDto>(project);
    }
}