using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Common.Dispatcher;
using FinanceTracker.Api.Infra.Clients.Keycloak;
using FinanceTracker.Api.Infra.Data;

namespace FinanceTracker.Api.Features.Users;
public record Request(string Email, string Username, string FirstName, string LastName, string Password);

public class CreateUserCommand(
    IKeycloakClient keycloakClient,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<Request, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(Request command, CancellationToken cancellation)
    {
        var user = new User(command.Username, command.Email, command.FirstName, command.LastName);

        var result = await keycloakClient.CreateUserAsync(command, user.Id, cancellation);

        if (!result.IsSuccess)
            return Result<Guid>.Failure(result.Error!);

        user.SetExternalId(result.Data);

        userRepository.Add(user);
        await unitOfWork.CommitAsync(cancellation);

        return Result<Guid>.Success(user.Id);
    }
};
public class CreateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync)
            .WithName("Create user")
            .WithDescription("Create a new user in the application")
            .Produces<Result<Guid>>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);

    public static async Task<IResult> HandleAsync(
        ICommandDispatcher dispatcher,
        Request request,
        CancellationToken cancellationToken)
    {
        var result = await dispatcher.Dispatch<Request, Result<Guid>>(request, cancellationToken);

        if (!result.IsSuccess)
            return Results.BadRequest(result.Error);

        return Results.Created($"api/v1/users/{result.Data}", result);
    }
}
