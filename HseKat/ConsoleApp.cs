public class ConsoleApp
{
    // get ParkService from DI
    private readonly ParkService _park;

    public ConsoleApp(ParkService park)
    {
        _park = park;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            Console.Write("Выберите действие: ");

            string? choice = Console.ReadLine();
            if (choice == null)
                return;

            switch (choice)
            {
                case "1":
                    AddTransport();
                    break;
                case "2":
                    AddThing();
                    break;
                case "3":
                    Console.WriteLine($"Всего объектов: {_park.GetCount()}");
                    break;
                case "4":
                    Console.WriteLine($"Общее суточное энергопотребление: {_park.GetTotalEnergyConsumption()}");
                    break;
                case "5":
                    ShowBeginnerTransport();
                    break;
                case "6":
                    ShowInventory();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неизвестный пункт меню.");
                    break;
            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1 - Добавить транспорт");
        Console.WriteLine("2 - Добавить вещь");
        Console.WriteLine("3 - Показать количество объектов");
        Console.WriteLine("4 - Показать суммарное энергопотребление");
        Console.WriteLine("5 - Показать транспорт для новичков");
        Console.WriteLine("6 - Показать весь инвентарь");
        Console.WriteLine("0 - Выход");
    }

    private void ShowBeginnerTransport()
    {
        var transports = _park.GetBeginnerTransport().ToList();
        if (transports.Count == 0)
        {
            Console.WriteLine("Транспорта для новичков пока нет.");
            return;
        }

        foreach (var transport in transports)
        {
            Console.WriteLine($"{transport.Name}: {transport.InventoryNumber}");
        }
    }

    private void ShowInventory()
    {
        var inventory = _park.GetInventory().ToList();
        if (inventory.Count == 0)
        {
            Console.WriteLine("Инвентарь пока пуст.");
            return;
        }

        foreach (var item in inventory)
        {
            Console.WriteLine($"{item.Name}: {item.InventoryNumber}");
        }
    }

    private void AddTransport()
    {
        Console.WriteLine("1 - Bicycle");
        Console.WriteLine("2 - Scooter");
        Console.WriteLine("3 - ElectricBike");
        string type = ReadText("Выберите тип транспорта: ");
        if (type != "1" && type != "2" && type != "3")
        {
            Console.WriteLine("Неизвестный тип транспорта.");
            return;
        }

        string inventoryNumber = ReadText("Инвентарный номер: ");
        string name = ReadText("Название: ");

        int easeOfUse = ReadInt("Удобство использования (1–10): ");

        Transport transport;
        if (type == "1")
        {
            transport = new Bicycle(inventoryNumber, name, easeOfUse);
        }
        else
        {
            double energy = ReadDouble("Суточное энергопотребление: ");
            if (type == "2")
            {
                transport = new Scooter(inventoryNumber, name, easeOfUse, energy);
            }
            else
            {
                transport = new ElectricBike(inventoryNumber, name, easeOfUse, energy);
            }
        }

        bool added = _park.AddTransport(transport);
        Console.WriteLine(added ? "Транспорт добавлен." : "Транспорт не прошёл техосмотр.");
    }

    private void AddThing()
    {
        Console.WriteLine("1 - Helmet");
        Console.WriteLine("2 - ChargingStation");
        string type = ReadText("Выберите тип вещи: ");
        if (type != "1" && type != "2")
        {
            Console.WriteLine("Неизвестный тип вещи.");
            return;
        }

        string inventoryNumber = ReadText("Инвентарный номер: ");
        string name = ReadText("Название: ");

        Thing thing;
        if (type == "1")
        {
            thing = new Helmet(inventoryNumber, name);
        }
        else
        {
            double energy = ReadDouble("Суточное энергопотребление: ");
            thing = new ChargingStation(inventoryNumber, name, energy);
        }

        bool added = _park.AddThing(thing);
        Console.WriteLine(added ? "Вещь добавлена." : "Некорректные данные вещи.");
    }

    private static string ReadText(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? "";
    }

    private static int ReadInt(string message)
    {
        while (true) // while user doesn't write correct number
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Console.WriteLine("Введите целое число.");
        }
    }

    private static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            if (double.TryParse(Console.ReadLine(), out double value) && value >= 0 && double.IsFinite(value))
                return value;

            Console.WriteLine("Введите неотрицательное число.");
        }
    }
}
