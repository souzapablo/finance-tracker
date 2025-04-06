using FinanceTracker.Api.Shared;

namespace FinanceTracker.Api.Entities;

public class Account(string name) : Entity
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public string Name { get; private set; } = name;
    public decimal Balance { get; private set; }
    public IEnumerable<Card> Cards { get; private set; } = [];
    public IEnumerable<Transaction> Transactions { get; set; } = [];
    public IEnumerable<Subscription> Subscriptions { get; private set; } = [];
    public IEnumerable<Installment> Installments { get; private set; } = [];
    public IEnumerable<Budget> Budgets { get; private set;} = [];
}
