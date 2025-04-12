using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Features.Users;
using FinanceTracker.Api.Infra.Clients.Keycloak;
using NSubstitute;

namespace FinanceTracker.UnitTests.Features.Users;
public class CreateUserTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IKeycloakClient _keycloakClient = Substitute.For<IKeycloakClient>();

    [Fact]
    public async Task ShouldCreateNewUser_WhenInputIsValid()
    {
        // Arrange
        var request = new Request("test@email.com", "test", "test", "test", "test");

        var command = new CreateUserCommand(_keycloakClient, _userRepository);

        _userRepository.InsertAsync(Arg.Any<User>(), CancellationToken.None)
            .Returns(1);

        _keycloakClient.CreateUserAsync(Arg.Any<Request>(), Arg.Any<long>(), CancellationToken.None)
            .Returns(Result<string>.Success(Guid.NewGuid().ToString()));

        // Act
        var result = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(1, result.Data);
    }

    [Fact]
    public async Task ShouldReturnExternalError_WhenInputIsInvalid()
    {
        // Arrange
        var request = new Request("test@email.com", "test", "test", "test", "test");

        var command = new CreateUserCommand(_keycloakClient, _userRepository);

        _keycloakClient.CreateUserAsync(Arg.Any<Request>(), Arg.Any<long>(), CancellationToken.None)
            .Returns(Result<string>.Failure(Error.ExternalError("External error message")));

        // Act
        var result = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal("EXTERNAL", result.Error?.Code);
    }
}
