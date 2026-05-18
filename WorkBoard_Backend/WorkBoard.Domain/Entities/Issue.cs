using WorkBoard.Domain.Enums;

namespace WorkBoard.Domain.Entities;

public sealed class Issue : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public IssuePriority Priority { get; set; }
    public string? Description { get; private set; }
    public decimal Order { get; private set; }

    public Guid ColumnId { get; private set; }
    public Column Column { get; private set; }
    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; }

    private Issue() { }

    internal Issue(Guid id, string title, Guid columnId, Guid projectId, decimal order, IssuePriority priority = IssuePriority.Medium, string? description = null)
    {
        Id = id;
        SetTitle(title);
        SetDescription(description);
        SetColumnId(columnId);
        SetOrder(order);

        Priority = priority;
        ProjectId = projectId;
    }

    public void Rename(string title)
    {
        SetTitle(title);
    }

    public void UpdateDescription(string? description)
    {
        SetDescription(description);
    }

    public void SetColumnId(Guid columnId)
    {
        if (columnId == Guid.Empty)
            throw new ArgumentException("ColumnId cannot be empty");

        ColumnId = columnId;
    }

    public void Move(Guid newColumnId, decimal newOrder)
    {
        ColumnId = newColumnId;
        Order = newOrder;
    }

    internal void SetOrder(decimal order)
    {
        if (order < 0)
            throw new ArgumentException("Issue order cannot be negative");

        Order = order;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Issue title cannot be empty");

        if (title.Length < 3)
            throw new ArgumentException("Issue title must be at least 3 characters long");

        if (title.Length > 200)
            throw new ArgumentException("Issue title cannot exceed 200 characters");

        Title = title;        
    }

    private void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description) 
            ? null 
            : description;
    }
}