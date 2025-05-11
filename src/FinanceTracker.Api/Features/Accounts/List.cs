using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Common.Dispatcher;
using FinanceTracker.Api.Extensions;
using System.Security.Claims;

namespace FinanceTracker.Api.Features.Accounts.List;

public record Request(Guid UserId);
public record Response(Guid Id, string Name, decimal Balance);

public class ListAccountsQuery(IAccountRepository accountRepository)
    : IQueryHandler<Request, Result<IEnumerable<Response>>>
{
    public async Task<Result<IEnumerable<Response>>> Handle(Request query, CancellationToken cancellation)
    {
        var accounts = await accountRepository.GetAllAsync(query.UserId, cancellation);

        var response = accounts.Select(x => x.ToResponse());

        return Result<IEnumerable<Response>>.Success(response);
    }
}

public class ListEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapGet("", HandleAsync)
            .WithName("List accounts")
            .WithDescription("List all the accounts related to the user logged in")
            .Produces<Result<IEnumerable<Response>>>(StatusCodes.Status200OK);

    public static async Task<IResult> HandleAsync(
        IQueryDispatcher dispatcher,
        ClaimsPrincipal claims,
        CancellationToken cancellationToken)
    {
        var request = new Request(claims.GetUserId());

        var result = await dispatcher.Dispatch<Request, Result<IEnumerable<Response>>>(request, cancellationToken);

        return Results.Ok(result);
    }
}

public static class Converter
{
    public static Response ToResponse(this Account account) =>
        new(account.Id, account.Name, account.Balance);
}
