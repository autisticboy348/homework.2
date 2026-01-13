using System;
using System.Collections.Generic;

namespace TextPluginProgram
{
    public interface ITextPlugin
    {
        string Name { get; }
        string Process(string input);
    }

    public class ToUpperPlugin : ITextPlugin
    {
        public string Name => "ToUpperPlugin";

        public string Process(string input)
        {
            if (input == null) return null;
            return input.ToUpperInvariant();
        }
    }

    public class SpaceRemoverPlugin : ITextPlugin
    {
        public string Name => "SpaceRemoverPlugin";

        public string Process(string input)
        {
            if (input == null) return null;
            return input.Replace(" ", string.Empty);
        }
    }

    public class ReversePlugin : ITextPlugin
    {
        public string Name => "ReversePlugin";

        public string Process(string input)
        {
            if (input == null) return null;
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }

    public class TextProcessor
    {
        public string Process(string input, List<ITextPlugin> plugins)
        {
            string result = input;
            if (plugins == null) return result;

            foreach (var plugin in plugins)
            {
                result = plugin.Process(result);
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Система обработки текста на плагинах ---");

            string original = "Hello World! This is a test.";
            Console.WriteLine("Исходная строка: {0}", original);
            
            var plugins = new List<ITextPlugin>
            {
                new ToUpperPlugin(),
                new SpaceRemoverPlugin(),
                new ReversePlugin()
            };

            Console.WriteLine("Примененные плагины:");
            foreach (var p in plugins)
            {
                Console.WriteLine(" - {0}", p.Name);
            }

            var processor = new TextProcessor();
            string result = processor.Process(original, plugins);

            Console.WriteLine("Результат после обработки: {0}", result);
        }
    }
}