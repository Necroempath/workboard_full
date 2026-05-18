using WorkBoard.Application.Features.Issues;

namespace WorkBoard.Application.Features.Columns;

public sealed class ColumnResponseDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Order { get; set; }
    public IssueResponseDto[] Issues { get; set; } = null!;
}
public sealed class CreateColumnRequest
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public sealed class UpdateColumnRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}