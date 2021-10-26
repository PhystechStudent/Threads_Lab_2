using System;
using System.Threading;

namespace ThreadsLab2
{
    internal class Program
    {
        public static readonly Mutex Mutex = new();

        private static void Main()
        {
            var warehouse = new Warehouse();
            var cart = new Cart();

            var firstProvider = new Provider(warehouse, cart);
            var secondProvider = new Provider(warehouse, cart);

            Thread.Sleep(1);

            var firstConsumer = new Consumer(cart);
            var secondConsumer = new Consumer(cart);
            var thirdConsumer = new Consumer(cart);

            while (cart.BallsCount > 0)
            {
                if (firstProvider.Thread.IsAlive == false)
                    firstProvider.InitializeThread();
                if (secondProvider.Thread.IsAlive == false)
                    secondProvider.InitializeThread();

                Thread.Sleep(1);

                if (firstConsumer.Thread.IsAlive == false)
                    firstConsumer.InitializeThread();
                if (secondConsumer.Thread.IsAlive == false)
                    secondConsumer.InitializeThread();
                if (thirdConsumer.Thread.IsAlive == false)
                    thirdConsumer.InitializeThread();
            }

            Console.WriteLine($"Количество шаров на складе - {warehouse.CurrentBallsCount}");
            Console.WriteLine($"Количество шаров в корзин - {cart.BallsCount}");
            Console.WriteLine($"Количество шаров у первого потребителя - {firstConsumer.BallsCount} \n" +
                              $"Количество шаров у второго потребителя - {secondConsumer.BallsCount} \n " +
                              $"Количество шаров у третьего потребителя - {thirdConsumer.BallsCount} \n " +
                              $"Суммарное количество шаров - {firstConsumer.BallsCount + secondConsumer.BallsCount + thirdConsumer.BallsCount}");
        }
    }
}