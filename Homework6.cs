using System;
using System.Collections.Generic;

namespace Auction
{
    public class Bidder
    {
        public string Name { get; }

        public Bidder(string name)
        {
            Name = name;
        }

        public void OnPriceChanged(object sender, PriceChangedEventArgs e)
        {
            if (e.Bidder == null || e.Bidder.Name == this.Name)
                return;

            Console.WriteLine("[{0}]: Новая ставка на '{1}' - {2.00:#0.##} (от {3}).",
                Name, e.LotName, e.NewPrice, e.Bidder.Name);
        }

        public override string ToString() => Name;
    }

    public class PriceChangedEventArgs : EventArgs
    {
        public string LotName { get; }
        public decimal NewPrice { get; }
        public Bidder Bidder { get; }

        public PriceChangedEventArgs(string lotName, decimal newPrice, Bidder bidder)
        {
            LotName = lotName;
            NewPrice = newPrice;
            Bidder = bidder;
        }
    }

    public class AuctionLot
    {
        public string LotName { get; }
        public decimal CurrentPrice { get; private set; }

        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public AuctionLot(string lotName, decimal startingPrice)
        {
            LotName = lotName;
            CurrentPrice = startingPrice;
        }

        public void PlaceBid(Bidder bidder, decimal newPrice)
        {
            if (newPrice > CurrentPrice)
            {
                CurrentPrice = newPrice;
                PriceChanged?.Invoke(this, new PriceChangedEventArgs(LotName, newPrice, bidder));
            }
            else
            {
                Console.WriteLine("Ставка от {0} не принята. Сумма должна быть выше {1:#0.##}.",
                    bidder.Name, CurrentPrice);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Аукцион начинается! Лот: 'Старинная ваза'. Начальная цена: 1000.00 ---");

            var lot = new AuctionLot("Старинная ваза", 1000m);

            var ivan = new Bidder("Иван");
            var petr = new Bidder("Петр");
            var anna = new Bidder("Анна");

            lot.PriceChanged += ivan.OnPriceChanged;
            lot.PriceChanged += petr.OnPriceChanged;
            lot.PriceChanged += anna.OnPriceChanged;

            Console.WriteLine("Участник '{0}' подписался на лот.", ivan.Name);
            Console.WriteLine("Участник '{0}' подписался на лот.", petr.Name);
            Console.WriteLine("Участник '{0}' подписался на лот.", anna.Name);

            ivan = ivan;
            Console.WriteLine("{0} делает ставку: 1200.00", ivan.Name);
            lot.PlaceBid(ivan, 1200m);

            Console.WriteLine("{0} делает ставку: 1500.00", anna.Name);
            lot.PlaceBid(anna, 1500m);

            Console.WriteLine("{0} делает ставку: 1300.00", petr.Name);
            lot.PlaceBid(petr, 1300m);
        }
    }

}
