using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Entities;
using FinanceTracker.Api.Features.Accounts;

namespace FinanceTracker.Api.Features.Users;

public class User(
    string username, 
    string email, 
    string firstName,
    string lastName) : Entity
{
    public string ExternalId { get; private set; } = string.Empty;
    public string Username { get; private set; } = username;
    public string Email { get; private set; } = email;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public IEnumerable<Account> Accounts { get; private set; } = [];
    public IEnumerable<Category> Categories { get; private set; } = [];

    public void SetExternalId(string externalId)
    {
        ExternalId = externalId;
        Update();
    }
}
