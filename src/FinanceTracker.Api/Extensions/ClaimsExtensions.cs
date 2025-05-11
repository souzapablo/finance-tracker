using System.Security.Claims;

namespace FinanceTracker.Api.Extensions;

public static class ClaimsExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claims)
    {
        var appUserId = claims.FindFirst("app_user_id")?.Value;

        if (!Guid.TryParse(appUserId, out var userId))
        {
            throw new InvalidOperationException("Invalid or missing 'app_user_id' claim.");
        }

        return userId;
    }
}
