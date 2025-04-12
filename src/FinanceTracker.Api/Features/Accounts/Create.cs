using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Common.Dispatcher;

namespace FinanceTracker.Api.Features.Accounts;

public record Request(string Name, long UserId);

public class CreateAccountCommand(
    IAccountRepository repository) : ICommandHandler<Request, Result<long>>
{
    public async Task<Result<long>> Handle(Request command, CancellationToken cancellation)
    {
        var account = new Account(command.Name, command.UserId);

        var accountId = await repository.InsertAsync(account, cancellation);

        return Result<long>.Success(accountId);
    }
}

public class CreateAccountEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync)
            .WithName("Create account")
            .WithDescription("Create a new account in the user wallet")
            .Produces<Result<long>>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);

    public static async Task<IResult> HandleAsync(
        Request request,
        ICommandDispatcher dispatcher,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher.Dispatch<Request, Result<long>>(request, cancellationToken);

        if (!result.IsSuccess)
            return Results.BadRequest(result.Error);

        return Results.Created($"api/v1/accounts/{result.Data}", result);
    }
}
