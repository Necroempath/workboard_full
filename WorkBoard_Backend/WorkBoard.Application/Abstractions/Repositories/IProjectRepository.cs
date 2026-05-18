using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IProjectRepository
{
    Task<Project> CreateAsync(Project project, CancellationToken token);
    Task DeleteAsync(Guid projectId, CancellationToken token);
    Task<Project?> GetByIdAsync(Guid projectId, CancellationToken token);
    Task<Column?> GetColumnByIdAsync(Guid columnId, CancellationToken token);
    Task SaveColumnAsync(Column column, CancellationToken token);
    Task<IEnumerable<Project>> GetByWorkspaceIdAsync(Guid workspaceId, CancellationToken token);
    Task SaveAsync(CancellationToken token);
}