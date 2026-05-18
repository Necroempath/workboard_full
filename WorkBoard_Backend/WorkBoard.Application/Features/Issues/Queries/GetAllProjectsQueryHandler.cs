using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Features.Projects;

namespace WorkBoard.Application.Features.Issues.Queries;

public sealed class GetAllIssuesQueryHandler : IRequestHandler<GetAllIssuesQuery, IEnumerable<IssueResponseDto>>
{
    private readonly IIssueRepository _issueRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public GetAllIssuesQueryHandler(IIssueRepository issueRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _issueRepository = issueRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IssueResponseDto>> Handle(GetAllIssuesQuery query, CancellationToken ct)
    {
        var issues = await _issueRepository.GetByProjectIdAsync(query.ProjectId, ct);

        return _mapper.Map<IEnumerable<IssueResponseDto>>(issues);
    }
}