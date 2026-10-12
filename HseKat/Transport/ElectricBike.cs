public class ElectricBike : Transport, IEnergyConsum
{
    public double DailyEnergyConsumption { get; }

    public ElectricBike(string inventoryNumber, string name, int easeOfUse, double dailyEnergyConsumption)
        : base(inventoryNumber, name, easeOfUse)
    {
        DailyEnergyConsumption = dailyEnergyConsumption;
    }
}
