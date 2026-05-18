using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IIssueRepository
{
    Task<Issue> CreateAsync(Issue issue, CancellationToken token);
    Task<bool> DeleteAsync(Guid issueId, CancellationToken token);
    Task<Issue?> GetByIdAsync(Guid issueId, CancellationToken token);
    Task<IEnumerable<Issue>> GetByProjectIdAsync(Guid projectId, CancellationToken token);
    Task<IEnumerable<Issue>> GetByColumnIdAsync(Guid columnId, CancellationToken token);
    Task SaveAsync(CancellationToken token);
}