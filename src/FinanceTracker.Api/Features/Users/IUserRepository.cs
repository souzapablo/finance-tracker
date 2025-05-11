namespace FinanceTracker.Api.Features.Users;

public interface IUserRepository
{
    Task<long> InsertAsync(User user, CancellationToken cancellation);
    Task UpdateExternalId(User user, long userId, CancellationToken cancellation);
}
