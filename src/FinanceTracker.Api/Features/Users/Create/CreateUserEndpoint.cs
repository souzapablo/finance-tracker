using FinanceTracker.Api.Common.Base;

namespace FinanceTracker.Api.Features.Users.Create;

public record CreateUserRequest(string Email, string Name);
public record CreateUserResponse(long Id);
public class CreateUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app) =>
        app.MapPost("", HandleAsync);

    public static async Task<IResult> HandleAsync(CreateUserRequest request)
    {
        return Results.Ok(new CreateUserResponse(1));
    }
}
