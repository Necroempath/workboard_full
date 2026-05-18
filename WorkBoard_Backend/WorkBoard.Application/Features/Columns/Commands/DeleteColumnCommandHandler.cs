using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Columns.Commands;

public sealed class DeleteColumnCommandHandler : IRequestHandler<DeleteColumnCommand, bool>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;

    public DeleteColumnCommandHandler(IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace)
    {
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
    }

    public async Task<bool> Handle(DeleteColumnCommand request, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can manage projects");

        var column = await _projectRepository.GetColumnByIdAsync(request.ColumnId, ct)
            ?? throw new InvalidOperationException("Invalid Column Id");

        if (column.Issues.Count > 0)
            throw new InvalidOperationException("Impossible to delete column with issues assigned to");

        column.Project.RemoveColumn(column.Id);

        await _projectRepository.SaveAsync(ct);

        return true;
    }
}
