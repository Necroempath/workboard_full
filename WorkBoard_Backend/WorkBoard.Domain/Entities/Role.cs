namespace WorkBoard.Domain.Entities;

public sealed class Role : BaseEntity
{
    private readonly List<UserRole> _userRoles = new();
    public string Name { get; private set; } = string.Empty;
    public IReadOnlyCollection<UserRole> UsersRoles => _userRoles;

    public const string User = "User";
    public const string Manager = "Manager";
    public const string Admin = "Admin";

    public Role(string name)
    {
        SetName(name);
    }

    public void Rename(string name)
    {
        SetName(name);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty");

        Name = name.Trim();
    }
}