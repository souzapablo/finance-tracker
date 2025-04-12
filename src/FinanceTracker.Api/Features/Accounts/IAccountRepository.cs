namespace FinanceTracker.Api.Features.Accounts;

public interface IAccountRepository
{
    Task<long> InsertAsync(Account account, CancellationToken cancellationToken);
}
