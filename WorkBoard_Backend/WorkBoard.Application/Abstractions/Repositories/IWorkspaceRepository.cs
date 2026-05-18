using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IWorkspaceRepository
{
    Task<Workspace> AddAsync(Workspace workspace, CancellationToken token);
    Task<Workspace?> GetByIdAsync(Guid id, Guid userId, CancellationToken token);
    Task<IEnumerable<Workspace>> GetAllAsync(Guid userId, CancellationToken token);
}
