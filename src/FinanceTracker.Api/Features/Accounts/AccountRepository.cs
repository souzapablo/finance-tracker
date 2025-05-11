using FinanceTracker.Api.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Api.Features.Accounts;

public class AccountRepository(FinanceTrackerDbContext context)
    : IAccountRepository
{
    public void Add(Account account) =>
        context.Accounts.Add(account);

    public async Task<IEnumerable<Account>> GetAllAsync(Guid userId, CancellationToken cancellation) =>
        await context.Accounts
            .Where(a => a.UserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellation);
}

public interface IAccountRepository
{
    void Add(Account account);
    Task<IEnumerable<Account>> GetAllAsync(Guid userId, CancellationToken cancellation);
}
