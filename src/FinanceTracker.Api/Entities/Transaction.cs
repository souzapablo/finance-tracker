using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Enums;
using FinanceTracker.Api.Features.Accounts;

namespace FinanceTracker.Api.Entities;

public class Transaction(
    Guid categoryId, 
    string description, 
    decimal amount,
    DateTime date, 
    TransactionType type) : Entity
{
    public Guid AccountId { get; private set; }
    public Account Account { get; private set; } = null!;
    public Guid? StatementId { get; private set; }
    public Statement? Statement { get; private set; }
    public Guid CategoryId { get; private set; } = categoryId;
    public Category Category { get; private set; } = null!;
    public string Description { get; private set; } = description;
    public decimal Amount { get; private set; } = amount;
    public DateTime Date { get; private set; } = date;
    public TransactionType Type { get; private set; } = type;
}
