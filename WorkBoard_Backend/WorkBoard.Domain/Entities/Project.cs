using System.Data.Common;

namespace WorkBoard.Domain.Entities;

public sealed class Project : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public Guid WorkspaceId { get; private set; }
    public Workspace Workspace { get; private set; }

    private readonly List<Column> _columns = new();
    public IReadOnlyCollection<Column> Columns => _columns;

    private readonly List<Issue> _issues = new();
    public IReadOnlyCollection<Issue> Issues => _issues;

    private Project() { }

    public Project(string name, Guid workspaceId)
    {
        SetName(name);
        SetWorkspaceId(workspaceId);
    }

    public void Rename(string name)
    {
        SetName(name);
    }

    public Column AddColumn(string name)
    {
        var column = new Column(name, Id, _columns.Count);

        _columns.Add(column);
        return column;
    }

    public void RemoveColumn(Guid columnId)
    {
        var column = _columns.FirstOrDefault(x => x.Id == columnId)
                     ?? throw new InvalidOperationException("Column not found");

        if (_columns.Count == 1)
            throw new InvalidOperationException("Project must have at least one column");

        _columns.Remove(column);
        ReorderColumns();
    }

    public void InitializeDefaultColumns()
    {
        _columns.Add(new Column("To Do", Id, 0));
        _columns.Add(new Column("In Progress", Id, 1));
        _columns.Add(new Column("Done", Id, 2));
    }

    private void ReorderColumns()
    {
        for (int i = 0; i < _columns.Count; i++)
        {
            _columns[i].SetOrder(i);
        }
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty");

        if (name.Length < 3)
            throw new ArgumentException("Project name must be at least 3 characters long");

        if (name.Length > 100)
            throw new ArgumentException("Project name cannot exceed 100 characters");

        Name = name;
    }

    private void SetWorkspaceId(Guid workspaceId)
    {
        if (workspaceId == Guid.Empty)
            throw new ArgumentException("WorkspaceId cannot be empty");

        WorkspaceId = workspaceId;
    }
}
