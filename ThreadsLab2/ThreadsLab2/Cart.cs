using System;

namespace ThreadsLab2
{
    public class Cart
    {
        public int BallsCount { get; private set; }

        public void AddBalls(int count)
        {
            if (count <= 0)
                throw new Exception("Количество мячей меньше или равно 0");

            BallsCount += count;
        }

        public int TakeBalls(int count = 1)
        {
            Program.Mutex.WaitOne();

            if (BallsCount <= 0)
            {
                Program.Mutex.ReleaseMutex();
                return -1;
            }

            if (count > BallsCount)
            {
                Console.WriteLine("Нет столько мячей в корзине!");
                Program.Mutex.ReleaseMutex();
                return BallsCount;
            }

            BallsCount -= count;

            Program.Mutex.ReleaseMutex();
            return count;
        }
    }
}