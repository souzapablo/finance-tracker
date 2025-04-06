using FinanceTracker.Api.Entities;

namespace FinanceTracker.UnitTests.Entities;
public class EntityTests
{
    [Fact]
    public void ShouldSetIsDeletedToTrue_WhenDeleted()
    {
        // Arrange
        var sut = new Account("Test Account");

        // Act
        sut.Delete();
        
        // Assert
        Assert.True(sut.IsDeleted);
    }

    [Fact]
    public void ShouldChangeLastUpdate_WhenUpdated()
    {
        // Arrange
        var sut = new Account("Test Account");
        var initialDate = sut.LastUpdate;

        // Act
        sut.Delete();

        // Assert
        Assert.NotEqual(initialDate, sut.LastUpdate);
    }
}
