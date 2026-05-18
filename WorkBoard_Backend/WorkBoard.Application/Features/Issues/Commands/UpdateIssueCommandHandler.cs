using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, IssueResponseDto>
{
    private readonly IIssueRepository _issueRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public UpdateIssueCommandHandler(IIssueRepository issueRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _issueRepository = issueRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<IssueResponseDto> Handle(UpdateIssueCommand command, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can update issues");

        var issue = await _issueRepository.GetByIdAsync(command.IssueId, ct)
          ?? throw new InvalidOperationException("Issue not found");

        issue.Rename(command.Request.Title);
        issue.UpdateDescription(command.Request.Description);
        issue.Priority = command.Request.Priority;

        await _issueRepository.SaveAsync(ct);

        return _mapper.Map<IssueResponseDto>(issue);
    }
}