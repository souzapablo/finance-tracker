using FinanceTracker.Api.Common.Base;

namespace FinanceTracker.Api.Features.Users;

public class Errors
{
    public static Error AlreadyRegistered =>
        new("USER_ALREADY_REGISTERED", "User is already registered in the database.");

    public static Error InvalidEmail =>
        new("INVALID_EMAIL", "Invalid e-mail.");

    public static Error InvalidPassword =>
        new("INVALID_PASSWORD", "Password must be at least 8 characters and include upper, lower, number, and special character.");

    public static Error InvalidUsername =>
        new("INVALID_USERNAME", "Username must be at least 3 characters.");

    public static Error InvalidFirstName =>
        new("INVALID_FIRST_NAME", "First name must be at least 2 characters.");

    public static Error InvalidLastName =>
        new("INVALID_LAST_NAME", "Last name must be at least 2 characters.");
}