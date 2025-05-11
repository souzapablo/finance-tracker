using FinanceTracker.Api.Infra.Data;

namespace FinanceTracker.Api.Features.Users;

public class UserRepository(FinanceTrackerDbContext context)
    : IUserRepository
{
    public void Add(User user) =>
        context.Users.Add(user);
}

public interface IUserRepository
{
    void Add(User user);
}
