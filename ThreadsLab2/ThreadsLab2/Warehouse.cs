using System;

namespace ThreadsLab2
{
    public class Warehouse
    {
        private const int StartBallCount = 10000;
        private int _currentBallsCount;

        public int CurrentBallsCount => _currentBallsCount;

        public Warehouse()
        {
            _currentBallsCount = StartBallCount;
        }

        public int TakeBalls(int count)
        {
            if (_currentBallsCount <= 0) return -1;

            if (count > _currentBallsCount)
            {
                Console.WriteLine("Нет столько мячей на складе!");
                count = _currentBallsCount;
                _currentBallsCount = 0;
                
                return count;
            }

            _currentBallsCount -= count;

            return count;
        }
    }
}