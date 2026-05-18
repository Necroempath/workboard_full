using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Features.Issues;

public sealed class IssueResponseDto
{
    public Guid Id { get; set; }
    public Guid ColumnId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IssuePriority Priority { get; set; }
    public decimal Order { get; set; }
}

public sealed class CreateIssueRequest
{
    public Guid ColumnId { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IssuePriority Priority { get; set; }
}

public sealed class UpdateIssueRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IssuePriority Priority { get; set; }
}

public sealed class MoveIssueRequest
{
    public Guid IssueId { get; set; }
    public Guid TargetColumnId { get; set; }
    public int TargetIndex { get; set; }
}