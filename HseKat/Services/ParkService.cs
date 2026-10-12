public class ParkService
{
    private readonly List<IItem> _items = new List<IItem>();
    private readonly IServiceCenter _serviceCenter;

    // DI (we don't create service center)
    public ParkService(IServiceCenter serviceCenter)
    {
        _serviceCenter = serviceCenter;
    }

    public bool AddTransport(Transport transport)
    {
        if (!_serviceCenter.Check(transport))
        {
            return false;
        }

        _items.Add(transport);
        return true;
    }

    public bool AddThing(Thing thing)
    {
        if (string.IsNullOrWhiteSpace(thing.InventoryNumber)
            || string.IsNullOrWhiteSpace(thing.Name))
        {
            return false;
        }

        _items.Add(thing);
        return true;
    }

    public int GetCount()
    {
        return _items.Count;
    }

    public double GetTotalEnergyConsumption()
    {
        return _items.OfType<IEnergyConsum>()
            .Sum(item => item.DailyEnergyConsumption);
    }

    public IEnumerable<Transport> GetBeginnerTransport()
    {
        return _items.OfType<Transport>()
            .Where(transport => transport.IsForBeginner());
    }

    public IEnumerable<IItem> GetInventory()
    {
        return _items;
    }
}
