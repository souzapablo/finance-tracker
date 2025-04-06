namespace FinanceTracker.Api.Shared;

public abstract class Entity
{
    public long Id { get; private set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}
