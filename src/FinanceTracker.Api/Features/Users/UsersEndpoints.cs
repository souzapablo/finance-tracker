using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Features.Users.Create;

namespace FinanceTracker.Api.Features.Users;

public static class UsersEndpoints
{
    public static void MapUsersEndpoints(this WebApplication app)
    {
        app.MapGroup("api/v1/users")
             .WithTags("Users")
             .MapEndpoint<CreateUserEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
    where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
