namespace WorkBoard.Domain.Entities;

public sealed class User : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;

    private readonly List<UserRole> _roles = new();
    public IReadOnlyCollection<UserRole> Roles => _roles;

    private readonly List<WorkspaceMembership> _memberships = new();
    public IReadOnlyCollection<WorkspaceMembership> Memberships => _memberships;

    private readonly List<RefreshToken> _refreshTokens = new();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;

    private readonly List<PasswordResetToken> _passwordResetTokens = new();
    public IReadOnlyCollection<PasswordResetToken> PasswordResetTokens => _passwordResetTokens;

    private User() { }

    public User(string name, string email, string passwordHash)
    {
        SetName(name);
        SetEmail(email);
        SetPasswordHash(passwordHash);
    }

    public void Rename(string name)
    {
        SetName(name);
    }

    public void UpdateEmail(string email)
    {
        SetEmail(email);
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        SetPasswordHash(passwordHash);
    }

    public void AssignRole(Role role)
    {
        if (HasRole(role.Name))
            return;

        _roles.Add(new UserRole(this, role));
    }

    public bool HasRole(string roleName)
    {
        return _roles.Any(r => r.Role.Name == roleName);
    }

    public void RemoveRole(Role role)
    {
        var userRole = _roles.FirstOrDefault(ur => ur.RoleId == role.Id);

        if (userRole != null)
            _roles.Remove(userRole);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name name cannot be empty");

        if (name.Length < 2)
            throw new ArgumentException("User name must be at least 2 characters long");

        if (name.Length > 100)
            throw new ArgumentException("User name cannot exceed 100 characters");

        Name = name;

    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        email = email.Trim().ToLowerInvariant();

        if (!email.Contains('@'))
            throw new ArgumentException("Invalid email format");

        Email = email;
    }

    private void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty");

        PasswordHash = passwordHash;
    }
}