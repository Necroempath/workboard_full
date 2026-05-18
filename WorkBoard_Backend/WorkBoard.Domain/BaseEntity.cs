namespace WorkBoard.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTimeOffset? UpdatedAt { get; protected set; }
    public DateTimeOffset? DeletedAt { get; protected set; }

    public void MarkUpdated() => UpdatedAt = DateTimeOffset.UtcNow;
    public void MarkDeleted() => DeletedAt = DateTimeOffset.UtcNow;
}
