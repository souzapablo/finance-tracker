using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Enums;

namespace FinanceTracker.Api.Entities;

public class Transaction(
    long categoryId, 
    string description, 
    decimal amount,
    DateTime date, 
    TransactionType type) : Entity
{
    public long AccountId { get; private set; }
    public Account Account { get; private set; } = null!;
    public long? StatementId { get; private set; }
    public Statement? Statement { get; private set; }
    public long CategoryId { get; private set; } = categoryId;
    public Category Category { get; private set; } = null!;
    public string Description { get; private set; } = description;
    public decimal Amount { get; private set; } = amount;
    public DateTime Date { get; private set; } = date;
    public TransactionType Type { get; private set; } = type;
}
