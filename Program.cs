using System;
using System.Collections.Generic;
public abstract class Employee
{
    public string Name { get; set; }
    public decimal BaseSalary { get; set; }

    protected Employee(string name, decimal baseSalary)
    {
        Name = name;
        BaseSalary = baseSalary;
    }

    public virtual decimal CalculateMonthlySalary()
    {
        return BaseSalary;
    }

    public virtual string GetPosition()
    {
        return "Сотрудник";
    }
}

public class Manager : Employee
{
    public decimal Bonus { get; set; }

    public Manager(string name, decimal baseSalary, decimal bonus)
        : base(name, baseSalary)
    {
        Bonus = bonus;
    }

    public override decimal CalculateMonthlySalary()
    {
        return BaseSalary + Bonus;
    }

    public override string GetPosition()
    {
        return "Менеджер";
    }
}

public class Developer : Employee
{
    public int LinesOfCodeWritten { get; set; }
    private const decimal RatePerLine = 2.5m;

    public Developer(string name, decimal baseSalary, int linesOfCodeWritten)
        : base(name, baseSalary)
    {
        LinesOfCodeWritten = linesOfCodeWritten;
    }

    public override decimal CalculateMonthlySalary()
    {
        decimal bonus = LinesOfCodeWritten * RatePerLine;
        return BaseSalary + bonus;
    }

    public override string GetPosition()
    {
        return "Разработчик";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Расчет заработной платы ---");

        List<Employee> employees = new List<Employee>();

        employees.Add(new Manager("Иван Петров", 100000, 5000));
        employees.Add(new Manager("Олег Васильев", 120000, 10000));

        employees.Add(new Developer("Анна Сидорова", 90000, 2100));
        employees.Add(new Developer("Мария Иванова", 95000, 5000));

        foreach (var employee in employees)
        {
            decimal salary = employee.CalculateMonthlySalary();
            Console.WriteLine($"Зарплата для {employee.GetPosition()} {employee.Name}: {salary:F0}");
        }

        Console.WriteLine("\n--- Детализированный расчет ---");
        foreach (var employee in employees)
        {
            if (employee is Manager manager)
            {
                Console.WriteLine($"{manager.GetPosition()} {manager.Name}:");
                Console.WriteLine($"  Базовая зарплата: {manager.BaseSalary:F0}");
                Console.WriteLine($"  Бонус: {manager.Bonus:F0}");
                Console.WriteLine($"  Итого: {manager.CalculateMonthlySalary():F0}\n");
            }
            else if (employee is Developer developer)
            {
                Console.WriteLine($"{developer.GetPosition()} {developer.Name}:");
                Console.WriteLine($"  Базовая зарплата: {developer.BaseSalary:F0}");
                Console.WriteLine($"  Написано строк кода: {developer.LinesOfCodeWritten}");
                Console.WriteLine($"  Премия за код: {developer.LinesOfCodeWritten * 2.5m:F0}");
                Console.WriteLine($"  Итого: {developer.CalculateMonthlySalary():F0}\n");
            }
        }
    }
}