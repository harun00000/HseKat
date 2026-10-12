public class ServiceCenterStub : IServiceCenter
{
    private readonly bool _result;

    public ServiceCenterStub(bool result)
    {
        _result = result;
    }

    public bool Check(Transport transport)
    {
        return _result;
    }
}

public class ParkServiceTests
{
    [Fact]
    public void AddGoodTransport()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));
        var scooter = new Scooter("1", "Scooter", 8, 2);

        // Act
        bool result = park.AddTransport(scooter);

        // Assert
        Assert.True(result);
        Assert.Equal(1, park.GetCount());
    }

    [Fact]
    public void DontAddBadTransport()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(false));
        var scooter = new Scooter("1", "Scooter", 8, 2);

        // Act
        bool result = park.AddTransport(scooter);

        // Assert
        Assert.False(result);
        Assert.Equal(0, park.GetCount());
    }

    [Fact]
    public void AddThing()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));
        var helmet = new Helmet("1", "Helmet");

        // Act
        bool added = park.AddThing(helmet);

        // Assert
        Assert.True(added);
        Assert.Equal(1, park.GetCount());
    }

    [Theory]
    [InlineData("", "Thing")]
    [InlineData("   ", "Thing")]
    [InlineData("1", "")]
    [InlineData("1", "   ")]
    public void RejectInvalidThing(string inventoryNumber, string name)
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));
        var helmet = new Helmet(inventoryNumber, name);
        var station = new ChargingStation(inventoryNumber, name, 4);

        // Act
        bool helmetAdded = park.AddThing(helmet);
        bool stationAdded = park.AddThing(station);

        // Assert
        Assert.False(helmetAdded);
        Assert.False(stationAdded);
        Assert.Equal(0, park.GetCount());
    }

    [Fact]
    public void CountEnergy()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));
        park.AddTransport(new Scooter("1", "Scooter", 42, 2));
        park.AddTransport(new ElectricBike("2", "Electric bike", 67, 3));
        park.AddThing(new Helmet("1945", "Helmet"));

        // Act
        double energy = park.GetTotalEnergyConsumption();

        // Assert
        Assert.Equal(5.0, energy);
    }

    [Fact]
    public void ZeroEnergy()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));
        park.AddTransport(new Bicycle("1", "Bicycle", 8));
        park.AddThing(new Helmet("2", "Helmet"));

        // Act
        double energy = park.GetTotalEnergyConsumption();

        // Assert
        Assert.Equal(0.0, energy);
    }

    [Fact]
    public void GetBeginnerTransport()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));
        park.AddTransport(new Bicycle("1", "Easy bicycle", 8));
        park.AddTransport(new Bicycle("2", "Hard bicycle", 4));

        // Act
        var transport = park.GetBeginnerTransport();

        // Assert
        var beginnerTransport = Assert.Single(transport);
        Assert.Equal(8, beginnerTransport.EaseOfUse);
    }

    [Fact]
    public void EaseSixWorks()
    {
        // Arrange
        var bicycle = new Bicycle("1", "Bicycle", 6);

        // Act
        bool result = bicycle.IsForBeginner();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAllItems()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));
        park.AddTransport(new Scooter("1", "Scooter", 8, 2));
        park.AddThing(new Helmet("2", "Helmet"));

        // Act
        var inventory = park.GetInventory();

        // Assert
        Assert.Equal(2, inventory.Count());
    }
}
