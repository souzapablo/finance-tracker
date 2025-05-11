using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Features.Accounts.Create;
using FinanceTracker.Api.Features.Accounts.List;

namespace FinanceTracker.Api.Features.Accounts;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        app.MapGroup("api/v1/accounts")
             .WithTags("Accounts")
             .RequireAuthorization()
             .MapEndpoint<CreateEndpoint>()
             .MapEndpoint<ListEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
    where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
