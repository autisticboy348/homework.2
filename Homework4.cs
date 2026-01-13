using System;
using System.Collections.Generic;

namespace CacheService
{
    public sealed class CacheService
    {
        private static readonly CacheService _instance = new CacheService();

        private readonly Dictionary<string, object> _cache;

        private CacheService()
        {
            _cache = new Dictionary<string, object>();
        }

        public static CacheService Instance => _instance;

        public void Add(string key, object value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            _cache[key] = value;
        }

        public object? Get(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            _cache.TryGetValue(key, out var value);
            return value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Демонстрация работы глобального кэша (Singleton) ---");

            CacheService cacheA = CacheService.Instance;
            CacheService cacheB = CacheService.Instance;

            Console.WriteLine("Добавляем данные в кэш через первую ссылку...");
            cacheA.Add("ConnectionString", "Server=.;Database=CacheDB;");
            cacheA.Add("ApiKey", "XYZ12345ABC");

            Console.WriteLine("Получаем данные из кэша через вторую ссылку...");
            var conn = cacheB.Get("ConnectionString");
            var api = cacheB.Get("ApiKey");

            Console.WriteLine("Значение по ключу 'ConnectionString': {0}", conn);
            Console.WriteLine("Значение по ключу 'ApiKey': {0}", api);

            Console.WriteLine("Проверяем, что обе переменные ссылаются на один объект...");
            bool sameInstance = object.ReferenceEquals(cacheA, cacheB);
            Console.WriteLine("Результат: {0}", sameInstance);
        }
    }
}
