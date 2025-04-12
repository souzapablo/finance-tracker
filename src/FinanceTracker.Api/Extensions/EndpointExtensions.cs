using FinanceTracker.Api.Features.Users;

namespace FinanceTracker.Api.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Hello World!");
        app.MapUsersEndpoints();
    }
}
