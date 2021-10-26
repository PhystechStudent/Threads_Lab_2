using System;
using System.Threading;

namespace ThreadsLab2
{
    public class Provider
    {
        private const int MinBallsValue = 1;
        private const int MaxBallsValue = 100;

        private readonly Warehouse _warehouse;
        private readonly Cart _cart;

        public Thread Thread { get; private set; }

        public Provider(Warehouse warehouse, Cart cart)
        {
            _warehouse = warehouse;
            _cart = cart;

            Thread = new Thread(TakeBallsFromWarehouse);
            Thread.Start();
        }

        public void InitializeThread()
        {
            Thread = new Thread(TakeBallsFromWarehouse);
            Thread.Start();
        }
        
        private void TakeBallsFromWarehouse()
        {
            Program.Mutex.WaitOne();

            var random = new Random();
            int ballsCount = random.Next(MinBallsValue, MaxBallsValue);

            int warehouseBalls = _warehouse.TakeBalls(ballsCount);
            if (warehouseBalls <= 0)
            {
                Program.Mutex.ReleaseMutex();
                return;
            }

            AddBallsToCart(warehouseBalls);

            Program.Mutex.ReleaseMutex();
        }

        private void AddBallsToCart(int count)
        {
            if (count <= 0)
                throw new Exception("Количество мячей меньше или равно 0");

            _cart.AddBalls(count);
        }
    }
}