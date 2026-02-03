using System;

public interface IMessageSender
{
    void Send(string message);
}

public class EmailSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка по Email: {message}");
    }
}

public class SmsSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка по SMS: {message}");
    }
}

public abstract class NotificationServiceFactory
{
    public abstract IMessageSender CreateSender();

    public void SendNotification(string message)
    {
        IMessageSender sender = CreateSender();
        sender.Send(message);
    }
}

public class EmailNotificationFactory : NotificationServiceFactory
{
    public override IMessageSender CreateSender()
    {
        return new EmailSender();
    }
}

public class SmsNotificationFactory : NotificationServiceFactory
{
    public override IMessageSender CreateSender()
    {
        return new SmsSender();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Гибкая система уведомлений ---");

        DemonstrateWithUserInput();

        Console.WriteLine("\n\n--- Демонстрация гибкости системы ---");
        DemonstrateFactoryPattern();
    }

    static void DemonstrateWithUserInput()
    {
        string notificationType;

        do
        {
            Console.Write("Какой тип уведомлений использовать? (email/sms): ");
            notificationType = Console.ReadLine()?.ToLower().Trim();

        } while (notificationType != "email" && notificationType != "sms");

        NotificationServiceFactory factory;

        if (notificationType == "email")
        {
            factory = new EmailNotificationFactory();
            Console.WriteLine("Создана фабрика для Email.");
        }
        else
        {
            factory = new SmsNotificationFactory();
            Console.WriteLine("Создана фабрика для SMS.");
        }

        Console.WriteLine("Отправляем уведомление...");
        Console.WriteLine(new string('-', 40));

        factory.SendNotification("Ваш заказ #123 успешно оформлен.");
        Console.WriteLine(new string('-', 40));
    }

    static void DemonstrateFactoryPattern()
    {
        string testMessage = "Тестовое уведомление";

        NotificationServiceFactory emailFactory = new EmailNotificationFactory();
        Console.WriteLine("\nИспользуем Email фабрику:");
        IMessageSender emailSender = emailFactory.CreateSender();
        emailSender.Send(testMessage);

        NotificationServiceFactory smsFactory = new SmsNotificationFactory();
        Console.WriteLine("\nИспользуем SMS фабрику:");
        IMessageSender smsSender = smsFactory.CreateSender();
        smsSender.Send(testMessage);

        Console.WriteLine("\n\n--- Демонстрация полиморфизма ---");

        NotificationServiceFactory[] factories =
        {
            new EmailNotificationFactory(),
            new SmsNotificationFactory(),
            new EmailNotificationFactory()
        };

        foreach (var factory in factories)
        {
            factory.SendNotification("Еще одно уведомление через полиморфизм");
        }
    }
}
