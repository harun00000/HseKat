public abstract class Transport : IItem     // abstract because we can create just types of transport
{
    public string InventoryNumber { get; }
    public string Name { get; }
    public int EaseOfUse { get; }

    protected Transport(string itemNumber, string name, int easeOfUse)
    {
        InventoryNumber = itemNumber;
        Name = name;
        EaseOfUse = easeOfUse;
    }

    public bool IsForBeginner()
    {
        return EaseOfUse >= 6;
    }
}
