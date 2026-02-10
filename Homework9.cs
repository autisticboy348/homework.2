csharp
using System;
using System.Collections.Generic;

namespace SimpleDataProcessor
{
    public abstract class DataProcessor
    {
        public void Process()
        {
            string data = ReadData();
            List<string> parsedData = ParseData(data);
            int count = AnalyzeData(parsedData);
            SaveReport(count);
        }

        protected abstract string ReadData();
        protected abstract List<string> ParseData(string rawData);
        
        protected virtual int AnalyzeData(List<string> data)
        {
            Console.WriteLine($"[Анализ]: Найдено {data.Count} записей.");
            return data.Count;
        }
        
        protected virtual void SaveReport(int count)
        {
            Console.WriteLine($"[Сохранение]: Отчет с {count} записями сохранен.\n");
        }
    }

    public class CsvDataProcessor : DataProcessor
    {
        protected override string ReadData()
        {
            Console.WriteLine("[Чтение]: Чтение CSV данных...");
            return "Алексей,Борис,Виктор,Дмитрий";
        }
        
        protected override List<string> ParseData(string rawData)
        {
            Console.WriteLine("[Парсинг]: Парсинг CSV...");
            return new List<string>(rawData.Split(','));
        }
    }

    public class XmlDataProcessor : DataProcessor
    {
        protected override string ReadData()
        {
            Console.WriteLine("[Чтение]: Чтение XML данных...");
            return "<items><item>Элемент1</item><item>Элемент2</item><item>Элемент3</item></items>";
        }
        
        protected override List<string> ParseData(string rawData)
        {
            Console.WriteLine("[Парсинг]: Парсинг XML...");
            var result = new List<string>();
            
            string[] parts = rawData.Split(new[] { "<item>", "</item>" }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string part in parts)
            {
                if (!part.Contains("<") && !part.Contains(">") && part.Length > 0)
                {
                    result.Add(part);
                }
            }
            
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DataProcessor csv = new CsvDataProcessor();
            DataProcessor xml = new XmlDataProcessor();
            
            Console.WriteLine("=== Обработка CSV ===");
            csv.Process();
            
            Console.WriteLine("=== Обработка XML ===");
            xml.Process();
        }
    }
}
