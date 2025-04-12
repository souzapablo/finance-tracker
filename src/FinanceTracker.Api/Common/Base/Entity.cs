namespace FinanceTracker.Api.Common;

public abstract class Entity
{
    public long Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public bool IsDeleted { get; private set; } = false;

    protected void Update() =>
        LastUpdate = DateTime.UtcNow;

    public void Delete()
    {
        LastUpdate = DateTime.UtcNow;
        IsDeleted = true;
    }
}
