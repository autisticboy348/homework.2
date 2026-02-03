using System;

public interface ILogger
{
    void Log(string message);
}

public class OldLogger
{
    public void LogMessage(string message, DateTime date)
    {
        Console.WriteLine($"[OldLogger]: {date:dd.MM.yyyy HH:mm:ss} - {message}");
    }

    public void OldMethod1() { /* ... */ }
    public void OldMethod2() { /* ... */ }
}
public class LoggerAdapter : ILogger
{
    private readonly OldLogger _oldLogger;

    public LoggerAdapter(OldLogger oldLogger)
    {
        _oldLogger = oldLogger ?? throw new ArgumentNullException(nameof(oldLogger));
    }

    public void Log(string message)
    {
        DateTime currentDate = DateTime.Now;

        _oldLogger.LogMessage(message, currentDate);
    }
}

public class LoggerAdapter2 : ILogger
{
    private readonly OldLogger _oldLogger = new OldLogger();

    public void Log(string message)
    {
        _oldLogger.LogMessage(message, DateTime.Now);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Демонстрация работы Адаптера ---");

        DemonstrateBasicAdapter();

        Console.WriteLine("\n\n--- Расширенная демонстрация ---");
        DemonstrateAdvancedUsage();

        Console.WriteLine("\n\n--- Демонстрация полиморфизма ---");
        DemonstratePolymorphism();
    }

    static void DemonstrateBasicAdapter()
    {
        Console.WriteLine("Вызов логгера через адаптер...");

        OldLogger oldLogger = new OldLogger();

        ILogger logger = new LoggerAdapter(oldLogger);

        logger.Log("Это тестовое сообщение для лога.");

        Console.WriteLine("\nПрямой вызов старого логгера:");
        oldLogger.LogMessage("Прямой вызов с ручной датой", new DateTime(2025, 10, 1, 1, 18, 0));
    }

    static void DemonstrateAdvancedUsage()
    {
        Console.WriteLine("Наше приложение использует только интерфейс ILogger:");

        // Создаем несколько адаптеров
        ILogger[] loggers = new ILogger[]
        {
            new LoggerAdapter(new OldLogger()),
            new LoggerAdapter2()
        };

        foreach (var logger in loggers)
        {
            logger.Log("Сообщение от современного приложения");
        }

        var application = new ModernApplication(new LoggerAdapter(new OldLogger()));
        application.DoSomethingImportant();
    }

    static void DemonstratePolymorphism()
    {
        Console.WriteLine("Использование разных реализаций через один интерфейс:");

        ILogger[] loggers = new ILogger[]
        {
            new LoggerAdapter(new OldLogger()),
            new ConsoleLogger(),
            new FileLogger("log.txt")
        };

        foreach (var logger in loggers)
        {
            logger.Log("Полиморфный вызов логгера");
        }
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[ConsoleLogger]: {DateTime.Now:HH:mm:ss} - {message}");
    }
}

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Log(string message)
    {
        Console.WriteLine($"[FileLogger]: Запись в файл {_filePath}: {message}");
    }
}

public class ModernApplication
{
    private readonly ILogger _logger;

    public ModernApplication(ILogger logger)
    {
        _logger = logger;
    }

    public void DoSomethingImportant()
    {
        Console.WriteLine("\nModernApplication выполняет важную операцию...");
        _logger.Log("Начало важной операции");
        _logger.Log("Завершение важной операции");
    }
}
