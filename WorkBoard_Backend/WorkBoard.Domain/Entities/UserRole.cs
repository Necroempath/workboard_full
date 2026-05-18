namespace WorkBoard.Domain.Entities;

public sealed class UserRole
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    private UserRole() { }

    public UserRole(User user, Role role)
    {
        User = user;
        Role = role;
    }
}
