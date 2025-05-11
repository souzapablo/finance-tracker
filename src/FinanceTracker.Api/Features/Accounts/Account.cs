using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Entities;

namespace FinanceTracker.Api.Features.Accounts;

public class Account(string name, long userId) : Entity
{
    public long UserId { get; private set; } = userId;
    public string Name { get; private set; } = name;
    public decimal Balance { get; private set; }
    public IEnumerable<Card> Cards { get; private set; } = [];
    public IEnumerable<Transaction> Transactions { get; set; } = [];
    public IEnumerable<Subscription> Subscriptions { get; private set; } = [];
    public IEnumerable<Installment> Installments { get; private set; } = [];
    public IEnumerable<Budget> Budgets { get; private set;} = [];
}
