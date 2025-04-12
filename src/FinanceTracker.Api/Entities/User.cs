using FinanceTracker.Api.Common.Base;

namespace FinanceTracker.Api.Entities;

public class User(
    string username, 
    string email, 
    string externalId,
    string firstName,
    string lastName) : Entity
{
    public string ExternalId { get; private set; } = externalId;
    public string Username { get; private set; } = username;
    public string Email { get; private set; } = email;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public IEnumerable<Account> Accounts { get; private set; } = [];
    public IEnumerable<Category> Categories { get; private set; } = [];
}
