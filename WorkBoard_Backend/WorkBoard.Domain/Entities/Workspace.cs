namespace WorkBoard.Domain.Entities;

public sealed class Workspace : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public Guid OwnerId { get; private set; }

    private readonly List<WorkspaceMembership> _members = new();
    public IReadOnlyCollection<WorkspaceMembership> Members => _members;

    private readonly List<Project> _projects = new();
    public IReadOnlyCollection<Project> Projects => _projects;

    private Workspace() { }

    public Workspace(string name, Guid ownerId)
    {
        SetName(name);

        SetOwnerId(ownerId);
    }

    public void Rename(string name)
    {
        SetName(name);
    }

    public void SetOwner(Guid ownerId)
    {
        SetOwnerId(ownerId);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Workspace name cannot be empty");

        if (name.Length < 3)
            throw new ArgumentException("Workspace name must be at least 3 characters long");

        if (name.Length > 100)
            throw new ArgumentException("Workspace name cannot exceed 100 characters");

        Name = name;
    }

    private void SetOwnerId(Guid ownerId)
    {
        if (ownerId == Guid.Empty)
            throw new ArgumentException("OwnerId cannot be empty");

        OwnerId = ownerId;
    }
}
