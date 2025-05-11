using FinanceTracker.Api.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Api.Features.Users;

public class UserRepository(FinanceTrackerDbContext context)
    : IUserRepository
{
    public void Add(User user) =>
        context.Users.Add(user);

    public async Task<bool> CheckIfExistsAsync(string email, string username, CancellationToken cancellationToken) =>
        await context.Users
            .AnyAsync(user => user.Email == email || user.Username == username, cancellationToken);

}

public interface IUserRepository
{
    void Add(User user);
    Task<bool> CheckIfExistsAsync(string email, string username, CancellationToken cancellationToken);
}
