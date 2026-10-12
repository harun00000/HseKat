public class ServiceCenter : IServiceCenter
{
    public bool Check(Transport transport)
    {
        return !string.IsNullOrWhiteSpace(transport.InventoryNumber)
            && !string.IsNullOrWhiteSpace(transport.Name)
            && transport.EaseOfUse >= 1
            && transport.EaseOfUse <= 10;
    }
}
