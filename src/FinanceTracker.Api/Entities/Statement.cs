using FinanceTracker.Api.Common.Base;

namespace FinanceTracker.Api.Entities;

public class Statement(
    DateTime closingDate, 
    DateTime dueDate, 
    decimal total, 
    bool isClosed,
    int cycle,
    int year, 
    int month) : Entity
{
    public Guid CardId { get; private set; }
    public Card Card { get; private set; } = null!;
    public DateTime ClosingDate { get; private set; } = closingDate;
    public DateTime DueDate { get; private set; } = dueDate;
    public decimal Total { get; set; } = total;
    public IEnumerable<Transaction> Transactions { get; private set; } = [];
    public bool IsClosed { get; private set; } = isClosed;
    public int Cycle { get; private set; } = cycle;
    public int Year { get; private set; } = year;
    public int Month { get; private set; } = month;
}
