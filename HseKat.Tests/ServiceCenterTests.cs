public class ServiceCenterTests
{
    [Theory]
    [InlineData("1", "Bicycle", 1, true)]
    [InlineData("1", "Bicycle", 10, true)]
    [InlineData("1", "Bicycle", 0, false)]
    [InlineData("1", "Bicycle", -1, false)]
    [InlineData("1", "Bicycle", 11, false)]
    [InlineData("", "Bicycle", 5, false)]
    [InlineData(" ", "Bicycle", 5, false)]
    [InlineData("1", "", 5, false)]
    [InlineData("1", " ", 5, false)]
    public void CheckTransport(string inventoryNumber, string name, int easeOfUse, bool expected)
    {
        // Arrange
        var serviceCenter = new ServiceCenter();
        var bicycle = new Bicycle(inventoryNumber, name, easeOfUse);

        // Act
        bool result = serviceCenter.Check(bicycle);

        // Assert
        Assert.Equal(expected, result);
    }
}
