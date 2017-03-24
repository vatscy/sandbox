using System;
using System.Linq;
using Vatscy.DesignPattern.Command;

namespace Vatscy.DesignPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new PatternCommand[]
            {
                new Strategy.Main(),
                new Visitor.Main(),
                new ChainOfResponsibility.Main()
            }
            .ForEach(c =>
            {
                Console.WriteLine();
                Console.WriteLine("[" + c.PatternName + "]");
                c.Execute();
            });
        }
    }
}