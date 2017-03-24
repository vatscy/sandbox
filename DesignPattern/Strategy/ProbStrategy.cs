using System;
using System.Linq;

namespace Vatscy.DesignPattern.Strategy
{
    public class ProbStrategy : IStrategy
    {
        private Random _random;

        private Hand _prevHandValue = Hand.GetHand(Hand.Guu);

        private Hand PrevHandValue
        {
            get
            {
                return _prevHandValue;
            }
        }

        private Hand _currentHandValue = Hand.GetHand(Hand.Guu);

        private Hand CurrentHandValue
        {
            get
            {
                return _currentHandValue;
            }
            set
            {
                _currentHandValue = value;
            }
        }

        private int[][] _history = new[]
        {
            new [] { 1, 1, 1 },
            new [] { 1, 1, 1 },
            new [] { 1, 1, 1 }
        };

        public ProbStrategy(int seed)
        {
            _random = new Random(seed);
            foreach (var hv1 in Hand.GetHands())
            {
                foreach (var hv2 in Hand.GetHands())
                {
                    _history[hv1][hv2] = 1;
                }
            }
        }

        public virtual Hand NextHand()
        {
            var bet = _random.Next(GetSum(CurrentHandValue));

            var handValue = Hand.Guu;
            if (bet < _history[CurrentHandValue][Hand.Guu])
            {
                handValue = Hand.Guu;
            }
            else if (bet < _history[CurrentHandValue][Hand.Guu] + _history[CurrentHandValue][Hand.Cho])
            {
                handValue = Hand.Cho;
            }
            else
            {
                handValue = Hand.Paa;
            }

            _prevHandValue = CurrentHandValue;
            CurrentHandValue = (Hand)handValue;
            return new Hand(handValue);
        }

        private int GetSum(Hand hv)
        {
            return _history[hv].Sum();
        }

        public virtual void Study(bool win)
        {
            if (win)
            {
                _history[PrevHandValue][CurrentHandValue]++;
            }
            else
            {
                _history[PrevHandValue][CurrentHandValue.NextHand]++;
                _history[PrevHandValue][CurrentHandValue.NextHand.NextHand]++;
            }
        }
    }
}