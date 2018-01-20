using System;
using Vatscy.DesignPattern.Command;

namespace Vatscy.DesignPattern.Strategy
{
    public class Main : PatternCommand
    {
        public Main() : base("Strategy") { }

        public override void Execute()
        {
            Console.Write("seed1: ");
            var seed1 = int.Parse(Console.ReadLine());
            Console.Write("seed2: ");
            var seed2 = int.Parse(Console.ReadLine());

            var player1 = new Player("Taro", new WinningStrategy(seed1));
            var player2 = new Player("Hana", new ProbStrategy(seed2));
            for (int i = 0; i < 200; i++)
            {
                var nextHand1 = player1.NextHand();
                var nextHand2 = player2.NextHand();
                Console.WriteLine("{0} {1}", nextHand1.ToString(), nextHand2.ToString());
                if (nextHand1.IsStrongerThan(nextHand2))
                {
                    player1.Win();
                    player2.Lose();
                    Console.WriteLine("Winner:" + player1);
                }
                else if (nextHand2.IsStrongerThan(nextHand1))
                {
                    player1.Lose();
                    player2.Win();
                    Console.WriteLine("Winner:" + player2);
                }
                else
                {
                    player1.Even();
                    player2.Even();
                    Console.WriteLine("Even...");
                }
            }

            Console.WriteLine("Total result:");
            Console.WriteLine(player1.ToString());
            Console.WriteLine(player2.ToString());
        }
    }
}