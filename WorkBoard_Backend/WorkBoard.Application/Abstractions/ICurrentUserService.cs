namespace WorkBoard.Application.Abstractions;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string? UserName { get; }
    string? UserEmail { get; }
    bool IsAuthenticated { get; }
}
