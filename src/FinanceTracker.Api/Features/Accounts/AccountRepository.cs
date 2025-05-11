using FinanceTracker.Api.Infra.Data;

namespace FinanceTracker.Api.Features.Accounts;

public class AccountRepository(FinanceTrackerDbContext context)
    : IAccountRepository
{
    public void Add(Account account) =>
        context.Accounts.Add(account);
}

public interface IAccountRepository
{
    void Add(Account account);  
}
