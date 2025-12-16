using System;
using System.Collections.Generic;

public record Product(int Id, string Name, decimal Price)
{
    public override string ToString() =>
        $"Product {{ Id = {Id}, Name = {Name}, Price = {Price:F2} }}";
}

public class Inventory
{
    private readonly List<Product> _products = new List<Product>();

    public int Count => _products.Count;

    public void AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (_products.Exists(p => p.Id == product.Id))
            throw new ArgumentException($"Товар с ID {product.Id} уже существует");

        _products.Add(product);
    }

    public Product FindProductById(int id)
    {
        return _products.Find(p => p.Id == id);
    }

    public IReadOnlyList<Product> GetAllProducts()
    {
        return _products.AsReadOnly();
    }

    public bool RemoveProduct(int id)
    {
        var product = FindProductById(id);
        if (product != null)
        {
            return _products.Remove(product);
        }
        return false;
    }

    public decimal CalculateTotalValue()
    {
        decimal total = 0;
        foreach (var product in _products)
        {
            total += product.Price;
        }
        return total;
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("--- Управление инвентарем ---");

        Inventory inventory = new Inventory();

        try
        {
            inventory.AddProduct(new Product(1, "Молоко", 80.50m));
            Console.WriteLine($"Добавлен товар: {inventory.FindProductById(1)}");

            inventory.AddProduct(new Product(2, "Хлеб", 40.00m));
            Console.WriteLine($"Добавлен товар: {inventory.FindProductById(2)}");

            inventory.AddProduct(new Product(3, "Сыр", 450.99m));
            Console.WriteLine($"Добавлен товар: {inventory.FindProductById(3)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении товара: {ex.Message}");
        }

        Console.WriteLine("\n--- Поиск товара с ID 2 ---");
        Product foundProduct = inventory.FindProductById(2);
        if (foundProduct != null)
        {
            Console.WriteLine($"Найден товар: {foundProduct}");
        }
        else
        {
            Console.WriteLine("Товар не найден");
        }

        Console.WriteLine("\n--- Поиск товара с ID 99 ---");
        Product notFoundProduct = inventory.FindProductById(99);
        if (notFoundProduct != null)
        {
            Console.WriteLine($"Найден товар: {notFoundProduct}");
        }
        else
        {
            Console.WriteLine("Товар с ID 99 не найден.");
        }

        Console.WriteLine("\n--- Дополнительная информация ---");
        Console.WriteLine($"Общее количество товаров: {inventory.Count}");
        Console.WriteLine($"Общая стоимость инвентаря: {inventory.CalculateTotalValue():F2}");

        Console.WriteLine("\n--- Все товары в инвентаре ---");
        var allProducts = inventory.GetAllProducts();
        foreach (var product in allProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\n--- Удаление товара с ID 1 ---");
        bool removed = inventory.RemoveProduct(1);
        Console.WriteLine($"Товар удален: {removed}");
        Console.WriteLine($"Текущее количество товаров: {inventory.Count}");
    }
}
