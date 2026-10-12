public abstract class Thing : IItem
{
    public string InventoryNumber { get; }
    public string Name { get; }

    protected Thing(string inventoryNumber, string name)
    {
        InventoryNumber = inventoryNumber;
        Name = name;
    }
}
