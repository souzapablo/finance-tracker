using FinanceTracker.Api.Common.Base;

namespace FinanceTracker.Api.Entities;

public class User(
    string name, 
    string email, 
    string externalId) : Entity
{
    public string ExternalId { get; private set; } = externalId;
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public IEnumerable<Account> Accounts { get; private set; } = [];
    public IEnumerable<Category> Categories { get; private set; } = [];
}
