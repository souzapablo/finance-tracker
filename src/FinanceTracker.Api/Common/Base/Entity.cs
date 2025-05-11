namespace FinanceTracker.Api.Common.Base;

public abstract class Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
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
