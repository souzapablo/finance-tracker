using FinanceTracker.Api.Shared;

namespace FinanceTracker.Api.Entities;

public class User(
    string name, 
    string email, 
    string password) : Entity
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
    public IEnumerable<Account> Accounts { get; private set; } = [];
    public IEnumerable<Category> Categories { get; private set; } = [];
}
