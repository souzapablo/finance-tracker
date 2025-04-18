﻿using FinanceTracker.Api.Enums;
using FinanceTracker.Api.Shared;

namespace FinanceTracker.Api.Entities;

public class Category(
    string name,
    string color,
    string icon,
    CategoryType type) : Entity
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public string Name { get; private set; } = name;
    public string Color { get; private set; } = color;
    public string Icon { get; private set; } = icon;
    public CategoryType Type { get; private set; } = type;
}
