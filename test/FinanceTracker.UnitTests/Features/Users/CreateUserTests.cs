using FinanceTracker.Api.Common.Base;
using FinanceTracker.Api.Features.Users;
using FinanceTracker.Api.Infra.Clients.Keycloak;
using FinanceTracker.Api.Infra.Data;
using NSubstitute;

namespace FinanceTracker.UnitTests.Features.Users;
public class CreateUserTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IKeycloakClient _keycloakClient = Substitute.For<IKeycloakClient>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    [Fact(DisplayName = "Create new user")]
    public async Task ShouldCreateNewUser_WhenInputIsValid()
    {
        // Arrange
        var request = new Request("test@email.com", "test", "test", "test", "test");

        var command = new CreateUserCommand(_keycloakClient, _userRepository, _unitOfWork);

        _keycloakClient.CreateUserAsync(Arg.Any<Request>(), Arg.Any<Guid>(), CancellationToken.None)
            .Returns(Result<string>.Success(Guid.NewGuid().ToString()));

        // Act
        var result = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);
    }

    [Fact(DisplayName = "Return external error when input is not valid")]
    public async Task ShouldReturnExternalError_WhenInputIsInvalid()
    {
        // Arrange
        var request = new Request("test@email.com", "test", "test", "test", "test");

        var command = new CreateUserCommand(_keycloakClient, _userRepository, _unitOfWork);

        _keycloakClient.CreateUserAsync(Arg.Any<Request>(), Arg.Any<Guid>(), CancellationToken.None)
            .Returns(Result<string>.Failure(Error.ExternalError("External error message")));

        // Act
        var result = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal("EXTERNAL", result.Error?.Code);
    }
}
