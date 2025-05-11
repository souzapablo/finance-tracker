using FinanceTracker.Api.Features.Accounts;
using FinanceTracker.Api.Infra.Data;
using NSubstitute;

namespace FinanceTracker.UnitTests.Features.Accounts;
public class CreateAccountTests
{
    private readonly IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    [Fact]
    public async Task ShouldCreateAccount_WhenInputIsValid()
    {
        // Arrange
        var request = new Request
        {
            UserId = Guid.NewGuid(),
            Name = "Account"
        };

        var command = new CreateAccountCommand(_accountRepository, _unitOfWork);

        // Act
        var result = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);
    }
}
