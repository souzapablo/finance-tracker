using FinanceTracker.Api.Common.Base;

namespace FinanceTracker.Api.Features.Users;

public class Errors
{
    public static Error AlreadyRegistered =>
        new("USER_ALREADY_REGISTERED", "User is already registered in the database.");
}
