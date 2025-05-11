using System.Security.Claims;

namespace FinanceTracker.Api.Extensions;

public static class ClaimsExtensions
{
    public static long GetUserId(this ClaimsPrincipal claims)
    {
        var appUserId = claims.FindFirst("app_user_id")?.Value;

        if (!long.TryParse(appUserId, out var userId))
        {
            throw new InvalidOperationException("Invalid or missing 'app_user_id' claim.");
        }

        return userId;
    }
}
