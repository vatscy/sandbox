using System;
using System.Collections.Generic;

namespace Vatscy.DesignPattern.Strategy
{
    public class Hand
    {
        public static readonly int Guu = 0;
        public static readonly int Cho = 1;
        public static readonly int Paa = 2;

        private static readonly string[] _names = new[] { "グー", "チョキ", "パー" };

        public static implicit operator int (Hand hand)
        {
            return hand.Value;
        }

        public static explicit operator Hand(int value)
        {
            return new Hand(value);
        }

        public static Hand GetHand(int handValue)
        {
            return new Hand(handValue);
        }

        public static IEnumerable<Hand> GetHands()
        {
            yield return new Hand(Guu);
            yield return new Hand(Cho);
            yield return new Hand(Paa);
        }

        private int _handValue;

        public Hand(int handValue)
        {
            if (handValue < 0 || 2 < handValue) throw new ArgumentException();
            _handValue = handValue;
        }

        public int Value
        {
            get
            {
                return _handValue;
            }
        }

        public string Name
        {
            get
            {
                return _names[_handValue];
            }
        }

        public Hand StrongHand
        {
            get
            {
                return (Hand)((_handValue + 2) % _names.Length);
            }
        }

        public Hand NextHand
        {
            get
            {
                return (Hand)((_handValue + 1) % _names.Length);
            }
        }

        public bool IsStrongerThan(Hand opposite)
        {
            return Fight(opposite) > 0;
        }

        public bool IsWeekerThan(Hand opposite)
        {
            return Fight(opposite) < 0;
        }

        private int Fight(Hand opposite)
        {
            if (this.Equals(opposite))
            {
                return 0;
            }
            else if (this.Equals(opposite.StrongHand))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Hand && _handValue == ((Hand)obj).Value;
        }

        public override int GetHashCode()
        {
            return _handValue.GetHashCode();
        }
    }
}