﻿using FinanceTracker.Api.Shared;

namespace FinanceTracker.Api.Entities;

public class Installment(
    decimal amount, 
    string description,
    int day,
    int number) : Entity
{
    public long? CardId { get; private set; }
    public Card? Card { get; private set; }
    public long AccountId { get; private set; }
    public Account? Account { get; private set; }
    public decimal Amount { get; private set; } = amount;
    public string Description { get; private set; } = description;
    public int Day { get; set; } = day;
    public int Number { get; private set; } = number;
}