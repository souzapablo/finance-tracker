using FinanceTracker.Api.Features.Accounts;
using FinanceTracker.Api.Features.Accounts.List;
using FinanceTracker.UnitTests.Faker;
using NSubstitute;

namespace FinanceTracker.UnitTests.Features.Accounts;
public class ListAccountsTests
{
    private readonly IAccountRepository _accountRepository = Substitute.For<IAccountRepository>();

    [Fact(DisplayName = "List accounts related to user")]
    public async Task ShouldReturnList_WhenUserHaveAccounts()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var accounts = AccountFaker.Generate(userId, 3);
        var request = new Request(userId);

        _accountRepository.GetAllAsync(userId, Arg.Any<CancellationToken>())
            .Returns(accounts);

        var query = new ListAccountsQuery(_accountRepository);

        // Act
        var result = await query.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Data.Count());
    }

    [Fact(DisplayName = "Empty list when user has no accounts")]
    public async Task ShouldReturnEmptyList_WhenUserHaveNoAccounts()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new Request(userId);

        _accountRepository.GetAllAsync(userId, Arg.Any<CancellationToken>())
            .Returns([]);

        var query = new ListAccountsQuery(_accountRepository);

        // Act
        var result = await query.Handle(request, CancellationToken.None);

        // Assert
        Assert.Empty(result.Data);
    }
}
