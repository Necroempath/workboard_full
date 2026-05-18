using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Projects.Commands;

public sealed record DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace)
    {
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can delete projects");

        await _projectRepository.DeleteAsync(request.ProjectId, ct);

        return true;
    }
}