namespace FinanceTracker.Api.Features.Users;

public interface IUserRepository
{
    Task<long> InsertAsync(User user, CancellationToken cancellation);
}
