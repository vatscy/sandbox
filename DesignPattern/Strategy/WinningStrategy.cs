using System;

namespace Vatscy.DesignPattern.Strategy
{
    public class WinningStrategy : IStrategy
    {
        private Random _random;
        private bool _won = false;
        private Hand _prevHand;

        public WinningStrategy(int seed)
        {
            _random = new Random(seed);
        }

        public virtual Hand NextHand()
        {
            if (!_won)
            {
                _prevHand = Hand.GetHand(_random.Next(3));
            }
            return _prevHand;
        }

        public virtual void Study(bool win)
        {
            _won = win;
        }
    }
}