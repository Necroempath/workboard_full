using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Extensions;
using WorkBoard.Domain.Shared;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed class MoveIssueCommandHandler : IRequestHandler<MoveIssueCommand, IssueResponseDto>
{
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public MoveIssueCommandHandler(ICurrentWorkspaceService currentWorkspace, IProjectRepository projectRepository,
        IIssueRepository issueRepository, IMapper mapper)
    {
        _currentWorkspace = currentWorkspace;
        _issueRepository = issueRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IssueResponseDto> Handle(MoveIssueCommand command, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanMoveIssues())
            throw new InvalidOperationException("Viewers can not move issues");

        var issue = await _issueRepository.GetByIdAsync(command.IssueId, ct)
          ?? throw new InvalidOperationException("Issue not found");

        var targetColumn = await _projectRepository.GetColumnByIdAsync(command.Request.TargetColumnId, ct)
           ?? throw new Exception("Invalid column");

        var (prev, next) = targetColumn.GetNeighbors(command.Request.TargetIndex);

        var newOrder = OrderHelper.GetNewOrder(prev, next);

        issue.Move(targetColumn.Id, newOrder);

        await _issueRepository.SaveAsync(ct);

        return _mapper.Map<IssueResponseDto>(issue);
    }
}
