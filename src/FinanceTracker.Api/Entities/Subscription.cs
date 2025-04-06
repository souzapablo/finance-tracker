using FinanceTracker.Api.Shared;

namespace FinanceTracker.Api.Entities;

public class Subscription(
    string description, 
    decimal amount, 
    int day, 
    bool isActive) : Entity
{
    public long CardId { get; private set; }
    public Card? Card { get; private set; }
    public long AccountId { get; private set; }
    public Account? Account { get; private set; }
    public string Description { get; private set; } = description;
    public decimal Amount { get; private set; } = amount;
    public int Day { get; private set; } = day;
    public bool IsActive { get; private set; } = isActive;
}
