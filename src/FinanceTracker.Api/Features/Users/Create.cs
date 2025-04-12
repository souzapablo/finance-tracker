using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Common.Dispatcher;
using FinanceTracker.Api.Infra.Clients.Keycloak;

namespace FinanceTracker.Api.Features.Users;
public record Request(string Email, string Username, string FirstName, string LastName, string Password);

public class CreateUserCommand(
    IKeycloakClient keycloakClient,
    IUserRepository repository) : ICommandHandler<Request, Result<long>>
{
    public async Task<Result<long>> Handle(Request command, CancellationToken cancellation)
    {
        var result = await keycloakClient.CreateUserAsync(command, cancellation);

        if (!result.IsSuccess)
            return Result<long>.Failure(result.Error!);

        var user = new User(command.Username, command.Email, result.Data, command.FirstName, command.LastName);

        var userId = await repository.InsertAsync(user, cancellation);

        return Result<long>.Success(userId);
    }
};
public class CreateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync)
            .WithName("Create user")
            .WithDescription("Create a new user in the application")
            .Produces<Result<long>>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);

    public static async Task<IResult> HandleAsync(
        ICommandDispatcher dispatcher,
        Request request,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher.Dispatch<Request, Result<long>>(request, cancellationToken);

        if (!result.IsSuccess)
            return Results.BadRequest(result.Error);

        return Results.Created($"api/v1/users/{result.Data}", result);
    }
}
