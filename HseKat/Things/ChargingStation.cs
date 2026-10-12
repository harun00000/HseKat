public class ChargingStation : Thing, IEnergyConsum
{
    public double DailyEnergyConsumption { get; }

    public ChargingStation(string inventoryNumber, string name, double dailyEnergyConsumption)
        : base(inventoryNumber, name)
    {
        DailyEnergyConsumption = dailyEnergyConsumption;
    }
}
