public class ConsoleAppTests
{
    [Fact]
    public void Exit()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));

        // Act
        string output = RunApp(park, "0\n3\n");

        // Assert
        Assert.Contains("0 - Выход", output);
        Assert.DoesNotContain("Всего объектов:", output);
        Assert.Equal(0, park.GetCount());
    }

    [Fact]
    public void AddBicycle()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));

        // Act
        string output = RunApp(park, "1\n1\nB001\nВелосипед\n4\n3\n0\n");

        // Assert
        Assert.Contains("Транспорт добавлен.", output);
        Assert.Contains("Всего объектов: 1", output);
        Assert.IsType<Bicycle>(Assert.Single(park.GetInventory()));
    }

    [Fact]
    public void AddHelmet()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));

        // Act
        string output = RunApp(park, "2\n1\nH001\nШлем\n6\n0\n");

        // Assert
        Assert.Contains("Вещь добавлена.", output);
        Assert.Contains("Шлем: H001", output);
        Assert.IsType<Helmet>(Assert.Single(park.GetInventory()));
    }

    [Fact]
    public void AddChargingStation()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));

        // Act
        string output = RunApp(park, "2\n2\nC001\nЗарядная станция\n4\n4\n0\n");

        // Assert
        Assert.Contains("Вещь добавлена.", output);
        Assert.Contains("Общее суточное энергопотребление: 4", output);
        Assert.Equal(4.0, park.GetTotalEnergyConsumption());
        Assert.IsType<ChargingStation>(Assert.Single(park.GetInventory()));
    }

    [Fact]
    public void RejectTransport()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(false));

        // Act
        string output = RunApp(park, "1\n1\nB001\nВелосипед\n4\n0\n");

        // Assert
        Assert.Contains("Транспорт не прошёл техосмотр.", output);
        Assert.DoesNotContain("Транспорт добавлен.", output);
        Assert.Equal(0, park.GetCount());
    }

    [Fact]
    public void RejectInvalidThing()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));

        // Act
        string output = RunApp(park, "2\n1\n\nШлем\n0\n");

        // Assert
        Assert.Contains("Некорректные данные вещи.", output);
        Assert.DoesNotContain("Вещь добавлена.", output);
        Assert.Equal(0, park.GetCount());
    }

    [Theory]
    [InlineData("-4")]
    [InlineData("NaN")]
    [InlineData("Infinity")]
    [InlineData("-Infinity")]
    public void RetryInvalidEnergy(string invalidEnergy)
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));
        string input = $"2\n2\nC001\nСтанция\n{invalidEnergy}\n0\n4\n0\n";

        // Act
        string output = RunApp(park, input);

        // Assert
        Assert.Contains("Введите неотрицательное число.", output);
        Assert.Contains("Общее суточное энергопотребление: 0", output);
        var station = Assert.IsType<ChargingStation>(Assert.Single(park.GetInventory()));
        Assert.Equal(0.0, station.DailyEnergyConsumption);
    }

    [Fact]
    public void ExitOnEndOfInput()
    {
        // Arrange
        var park = new ParkService(new ServiceCenterStub(true));

        // Act
        string output = RunApp(park, "");

        // Assert
        Assert.Equal(0, park.GetCount());
        Assert.DoesNotContain("Транспорт добавлен.", output);
        Assert.DoesNotContain("Вещь добавлена.", output);
        Assert.DoesNotContain("Неизвестный пункт меню.", output);
    }

    private static string RunApp(ParkService park, string input)
    {
        // save original console
        TextReader originalInput = Console.In;
        TextWriter originalOutput = Console.Out;

        // create new 
        using var reader = new StringReader(input);
        using var writer = new StringWriter();

        try
        {
            // read from reader, write from writer
            Console.SetIn(reader);
            Console.SetOut(writer);

            // run
            var app = new ConsoleApp(park);
            app.Run();

            // return as 1 big string
            return writer.ToString();
        }
        finally
        {
            // clean
            Console.SetIn(originalInput);
            Console.SetOut(originalOutput);
        }
    }
}
