using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Common.Dispatcher;
using FinanceTracker.Api.Extensions;
using FinanceTracker.Api.Infra.Data;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace FinanceTracker.Api.Features.Accounts;

public class Request
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
};

public class CreateAccountCommand(
    IAccountRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<Request, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(Request command, CancellationToken cancellation)
    {
        var account = new Account(command.Name, command.UserId);

        repository.Add(account);
        await unitOfWork.CommitAsync(cancellation);

        return Result<Guid>.Success(account.Id);
    }
}

public class CreateAccountEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync)
            .WithName("Create account")
            .WithDescription("Create a new account in the user wallet")
            .Produces<Result<Guid>>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);

    public static async Task<IResult> HandleAsync(
        Request request,
        ICommandDispatcher dispatcher,
        ClaimsPrincipal claims,
        CancellationToken cancellationToken)
    {
        request.UserId = claims.GetUserId();
        var result = await dispatcher.Dispatch<Request, Result<Guid>>(request, cancellationToken);

        if (!result.IsSuccess)
            return Results.BadRequest(result.Error);

        return Results.Created($"api/v1/accounts/{result.Data}", result);
    }
}
