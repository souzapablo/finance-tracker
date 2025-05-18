using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Common.Dispatcher;
using FinanceTracker.Api.Extensions;
using FinanceTracker.Api.Infra.Clients.Keycloak;
using FinanceTracker.Api.Infra.Data;
using System.Text.RegularExpressions;

namespace FinanceTracker.Api.Features.Users;

public class CreateUserCommand(
    IKeycloakClient keycloakClient,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<Request, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(Request command, CancellationToken cancellation)
    {
        var userExists = await userRepository.CheckIfExistsAsync(command.Email, command.Username, cancellation);

        if (userExists)
            return Result<Guid>.Failure(Errors.AlreadyRegistered);

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
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces<ValidationResult>(StatusCodes.Status422UnprocessableEntity);

    public static async Task<IResult> HandleAsync(
        ICommandDispatcher dispatcher,
        Request request,
        CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return request.ValidationErrors.ToResult();

        var result = await dispatcher.Dispatch<Request, Result<Guid>>(request, cancellationToken);

        if (!result.IsSuccess)
            return Results.BadRequest(result);

        return Results.Created($"api/v1/users/{result.Data}", result);
    }
}

public record Request(string Email, string Username, string FirstName, string LastName, string Password)
{
    private List<Error>? _validationErrors;

    public IReadOnlyCollection<Error> ValidationErrors =>
        _validationErrors ??= ValidateInternal();

    public bool IsValid() => ValidationErrors?.Count == 0;

    private List<Error> ValidateInternal()
    {
        var errors = new List<Error>();

        if (!IsEmailValid(Email))
            errors.Add(Errors.InvalidEmail);

        if (!IsPasswordValid(Password))
            errors.Add(Errors.InvalidPassword);

        if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3)
            errors.Add(Errors.InvalidUsername);

        if (string.IsNullOrWhiteSpace(FirstName) || FirstName.Length < 2)
            errors.Add(Errors.InvalidFirstName);

        if (string.IsNullOrWhiteSpace(LastName) || LastName.Length < 2)
            errors.Add(Errors.InvalidLastName);

        return errors;
    }
    private static bool IsPasswordValid(string password)
    {
        string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";
        return Regex.Match(password, pattern).Success;
    }

    private static bool IsEmailValid(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
};
