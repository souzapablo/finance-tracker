using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Features.Users;

namespace FinanceTracker.Api.Entities;

public class Budget(
    decimal amount,
    decimal remainingAmount, 
    DateTime month) : Entity
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public long CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public decimal Amount { get; private set; } = amount;
    public decimal RemainingAmount { get; private set; } = remainingAmount;
    public DateTime Month { get; private set; } = month;
}
