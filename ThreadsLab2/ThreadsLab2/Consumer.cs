using System.Threading;

namespace ThreadsLab2
{
    public class Consumer
    {
        private readonly Cart _cart;
        
        public int BallsCount { get; private set; }
        public Thread Thread { get; private set; }

        public Consumer(Cart cart)
        {
            _cart = cart;

            Thread = new Thread(TakeBalls);
            Thread.Start();
        }

        public void InitializeThread()
        {
            Thread = new Thread(TakeBalls);
            Thread.Start();
        }
        
        private void TakeBalls()
        {
            Program.Mutex.WaitOne();

            int count = _cart.TakeBalls();
            if (count <= 0)
            {
                Program.Mutex.ReleaseMutex();
                return;
            }

            BallsCount += count;
            Program.Mutex.ReleaseMutex();
        }
    }
}