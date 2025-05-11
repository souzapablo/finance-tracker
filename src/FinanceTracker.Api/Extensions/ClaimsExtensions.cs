using System.Security.Claims;

namespace FinanceTracker.Api.Extensions;

public static class ClaimsExtensions
{
    public static long GetUserId(this ClaimsPrincipal claims)
    {
        var appUserId = claims.FindFirst("app_user_id")?.Value;

        _ = long.TryParse(appUserId, out var userId);

        return userId;
    }
}
