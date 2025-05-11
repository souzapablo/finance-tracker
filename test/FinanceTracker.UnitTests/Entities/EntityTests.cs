using FinanceTracker.Api.Features.Accounts;

namespace FinanceTracker.UnitTests.Entities;
public class EntityTests
{
    [Fact]
    public void ShouldSetIsDeletedToTrue_WhenDeleted()
    {
        // Arrange
        var sut = new Account("Test Account", Guid.NewGuid());

        // Act
        sut.Delete();
        
        // Assert
        Assert.True(sut.IsDeleted);
    }

    [Fact]
    public void ShouldChangeLastUpdate_WhenUpdated()
    {
        // Arrange
        var sut = new Account("Test Account", Guid.NewGuid());
        var initialDate = sut.LastUpdate;

        // Act
        sut.Delete();

        // Assert
        Assert.NotEqual(initialDate, sut.LastUpdate);
    }
}
