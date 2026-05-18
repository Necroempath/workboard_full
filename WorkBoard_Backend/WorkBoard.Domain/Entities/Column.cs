using WorkBoard.Domain.Enums;
using WorkBoard.Domain.Shared;

namespace WorkBoard.Domain.Entities;

public sealed class Column : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public decimal Order { get; private set; }
    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; }

    private readonly List<Issue> _issues = new();
    public IReadOnlyCollection<Issue> Issues => _issues;

    private Column() { }

    internal Column(string name, Guid projectId, int order)
    {
        Id = Guid.NewGuid();
        SetName(name);
        SetProjectId(projectId);
        SetOrder(order);
    }

    public void Rename(string name)
    {
        SetName(name);
    }

    internal void SetOrder(int order)
    {
        if (order < 0)
            throw new ArgumentException("Column order cannot be negative");

        Order = order;
    }

    public Issue AddIssue(string title, string? description, IssuePriority priority, Guid projectId)
    {
        var lastOrder = _issues
            .OrderBy(i => i.Order)
            .LastOrDefault()?.Order;

        var newOrder = OrderHelper.GetNewOrder(lastOrder, null);

        var issue = new Issue(Guid.NewGuid(), title, Id, projectId, newOrder, priority, description);

        _issues.Add(issue);

        return issue;
    }

    public (decimal? prev, decimal? next) GetNeighbors(int index)
    {
        var ordered = _issues.OrderBy(i => i.Order).ToList();

        decimal? prev = index > 0 ? ordered[index - 1].Order : null;
        decimal? next = index < ordered.Count ? ordered[index].Order : null;

        return (prev, next);
    }

    public void RemoveIssue(Guid issueId)
    {
        var issue = _issues.FirstOrDefault(x => x.Id == issueId)
                    ?? throw new InvalidOperationException("Issue not found");

        issue.MarkDeleted();

        _issues.Remove(issue);
        ReorderIssues();
    }

    private void ReorderIssues()
    {
        for (int i = 0; i < _issues.Count; i++)
        {
            _issues[i].SetOrder(i);
        }
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Column name cannot be empty");

        if (name.Length < 2)
            throw new ArgumentException("Column name must be at least 2 characters long");

        if (name.Length > 50)
            throw new ArgumentException("Column name cannot exceed 50 characters");

        Name = name;
    }

    private void SetProjectId(Guid projectId)
    {
        if (projectId == Guid.Empty)
            throw new ArgumentException("ProjectId cannot be empty");

        ProjectId = projectId;
    }
}
