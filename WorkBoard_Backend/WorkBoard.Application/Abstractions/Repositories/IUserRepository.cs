using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken token);
    Task<User?> GetByEmailAsync(string email, CancellationToken token);
    Task<User> AddAsync(User user, CancellationToken token);
    Task SaveAsync(CancellationToken token);
}
    