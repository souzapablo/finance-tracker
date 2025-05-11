using FinanceTracker.Api.Features.Accounts;
using NSubstitute;

namespace FinanceTracker.UnitTests.Features.Accounts;
public class CreateAccountTests
{
    private readonly IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();

    [Fact]
    public async Task ShouldCreateAccount_WhenInputIsValid()
    {
        // Arrange
        var request = new Request
        {
            UserId = 1L,
            Name = "Account"
        };

        var command = new CreateAccountCommand(_accountRepository);

        _accountRepository.InsertAsync(Arg.Any<Account>(), CancellationToken.None)
            .Returns(2L);

        // Act
        var result = await command.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(2L, result.Data);
    }
}
