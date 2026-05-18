using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed record DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand, bool>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;

    public DeleteIssueCommandHandler(IIssueRepository issueRepository, IProjectRepository projectRepository,
        ICurrentWorkspaceService currentWorkspace)
    {
        _issueRepository = issueRepository;
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
    }

    public async Task<bool> Handle(DeleteIssueCommand request, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can delete issues");

        var issue = await _issueRepository.GetByIdAsync(request.IssueId, ct)
            ?? throw new InvalidOperationException("Issue not found");

        issue.Column.RemoveIssue(issue.Id);

        await _issueRepository.SaveAsync(ct);

        return true;
    }
}