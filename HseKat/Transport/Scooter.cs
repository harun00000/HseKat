public class Scooter : Transport, IEnergyConsum
{
    public double DailyEnergyConsumption { get; }

    public Scooter(string inventoryNumber, string name, int easeOfUse, double dailyEnergyConsumption)
        : base(inventoryNumber, name, easeOfUse)
    {
        DailyEnergyConsumption = dailyEnergyConsumption;
    }
}
