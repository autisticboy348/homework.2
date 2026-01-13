using System;
using System.Collections.Generic;

namespace DocumentRendering
{
    public abstract class Document
    {
        public string Author { get; set; }

        protected Document(string author)
        {
            Author = author;
        }

        public abstract void Render();
    }

    public class TextDocument : Document
    {
        public string Content { get; set; }

        public TextDocument(string author, string content)
            : base(author)
        {
            Content = content;
        }

        public override void Render()
        {
            Console.WriteLine("[Текст] Автор: {0}", Author);
            Console.WriteLine("Содержимое: {0}", Content);
        }
    }

    public class ImageDocument : Document
    {
        public string Resolution { get; set; }

        public ImageDocument(string author, string resolution)
            : base(author)
        {
            Resolution = resolution;
        }

        public override void Render()
        {
            Console.WriteLine("[Изображение] Автор: {0}", Author);
            Console.WriteLine("Рендеринг изображения с разрешением {0}", Resolution);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Рендеринг документов ---");

            List<Document> documents = new List<Document>();

            documents.Add(new TextDocument("Лев Толстой", "Все счастливые семьи похожи друг на друга..."));
            documents.Add(new ImageDocument("Иван Шишкин", "3558x2180"));
            documents.Add(new TextDocument("Михаил Булгаков", "В белом плаще с кровавым подбоем..."));

            foreach (var doc in documents)
            {
                doc.Render();
                Console.WriteLine("--------------------");
            }
        }
    }
}
