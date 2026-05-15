// --- Сначала код программы ---
try
{
    Console.WriteLine("Выберите категорию товара:");
    Console.WriteLine("1 - Продукт питания");
    Console.WriteLine("2 - Бытовая химия");
    Console.Write("Ваш выбор: ");
    string choice = Console.ReadLine();

    Console.Write("Наименование: ");
    string name = Console.ReadLine();

    Console.Write("Производитель: ");
    string manufacturer = Console.ReadLine();

    Console.Write("Цена: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        throw new ArgumentException("Неверный формат цены");

    Console.Write("Срок годности (дней): ");
    if (!int.TryParse(Console.ReadLine(), out int shelfLifeDays))
        throw new ArgumentException("Неверный формат срока годности");

    Console.Write("Дата производства (гггг-мм-дд): ");
    if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly productionDate))
        throw new ArgumentException("Неверный формат даты производства");

    Product product;

    if (choice == "1")
        product = new FoodProduct(name, manufacturer, price, shelfLifeDays, productionDate);
    else
        product = new ChemistryProduct(name, manufacturer, price, shelfLifeDays, productionDate);

    Console.WriteLine("\nИнформация о товаре:");
    Console.WriteLine(product);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"\nОшибка ввода: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"\nНепредвиденная ошибка: {ex.Message}");
}

// --- Потом объявления классов ---

abstract class Product
{
    private string _name;
    private string _manufacturer;
    private decimal _price;
    private int _shelfLifeDays;
    private DateOnly _productionDate;

    public string Name => _name;
    public string Manufacturer => _manufacturer;
    public decimal Price => _price;
    public int ShelfLifeDays => _shelfLifeDays;
    public DateOnly ProductionDate => _productionDate;

    public Product(string name, string manufacturer, decimal price, int shelfLifeDays, DateOnly productionDate)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Наименование не может быть пустым");

        if (string.IsNullOrEmpty(manufacturer))
            throw new ArgumentException("Производитель не может быть пустым");

        if (price <= 0)
            throw new ArgumentException("Цена должна быть положительной");

        if (shelfLifeDays <= 0)
            throw new ArgumentException("Срок годности должен быть положительным");

        if (productionDate > DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("Дата производства не может быть в будущем");

        _name = name;
        _manufacturer = manufacturer;
        _price = price;
        _shelfLifeDays = shelfLifeDays;
        _productionDate = productionDate;
    }

    public abstract string GetCategory();

    public virtual string GetExpirationInfo()
    {
        DateOnly expirationDate = _productionDate.AddDays(_shelfLifeDays);
        return "Годен до: " + expirationDate;
    }

    public override string ToString()
    {
        return $"Категория: {GetCategory()}\n" +
               $"Наименование: {_name}\n" +
               $"Производитель: {_manufacturer}\n" +
               $"Цена: {_price:0.00} руб.\n" +
               $"Дата производства: {_productionDate}\n" +
               $"Срок годности: {_shelfLifeDays} дней\n" +
               GetExpirationInfo();
    }
}

class FoodProduct : Product
{
    public FoodProduct(string name, string manufacturer, decimal price, int shelfLifeDays, DateOnly productionDate)
        : base(name, manufacturer, price, shelfLifeDays, productionDate)
    {
    }

    public override string GetCategory()
    {
        return "Продукт питания";
    }

    public override string GetExpirationInfo()
    {
        DateOnly expirationDate = ProductionDate.AddDays(ShelfLifeDays);
        bool isExpired = DateOnly.FromDateTime(DateTime.Today) > expirationDate;
        return "Годен до: " + expirationDate + (isExpired ? " (ПРОСРОЧЕН)" : " (в норме)");
    }
}

class ChemistryProduct : Product
{
    public ChemistryProduct(string name, string manufacturer, decimal price, int shelfLifeDays, DateOnly productionDate)
        : base(name, manufacturer, price, shelfLifeDays, productionDate)
    {
    }

    public override string GetCategory()
    {
        return "Бытовая химия";
    }
}