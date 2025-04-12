using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Enums;
using FinanceTracker.Api.Features.Accounts;

namespace FinanceTracker.Api.Entities;

public class Card(
    string name,
    CardBrand brand,
    decimal limit,
    int closingDay,
    int dueDay) : Entity
{
    public long AccountId { get; private set; }
    public Account Account { get; private set; } = null!;
    public string Name { get; private set; } = name;
    public CardBrand Brand { get; private set; } = brand;
    public decimal Limit { get; private set; } = limit;
    public decimal AvailableLimit { get; private set; }
    public int ClosingDay { get; private set; } = closingDay;
    public int DueDay { get; private set; } = dueDay;
    public IEnumerable<Statement> Statements { get; private set; } = [];
    public IEnumerable<Subscription> Subscriptions { get; private set; } = [];
    public IEnumerable<Installment> Installments { get; private set; } = [];
}
