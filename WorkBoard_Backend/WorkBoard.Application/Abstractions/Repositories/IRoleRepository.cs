using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id, CancellationToken token);
    Task<Role?> GetByNameAsync(string roleName, CancellationToken token);
    Task<Role> AddAsync(Role role, CancellationToken token);
}