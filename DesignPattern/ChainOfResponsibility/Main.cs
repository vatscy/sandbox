using Vatscy.DesignPattern.Command;

namespace Vatscy.DesignPattern.ChainOfResponsibility
{
    public class Main : PatternCommand
    {
        public Main() : base("ChainOfResponsibility") { }

        public override void Execute()
        {
            var alice = new NoSupport("Alice");
            var bob = new LimitSupport("Bob", 100);
            var charlie = new SpecialSupport("Charlie", 429);
            var diana = new LimitSupport("Diana", 200);
            var elmo = new OddSupport("Elmo");
            var fred = new LimitSupport("Fred", 300);

            alice.SetNext(bob).SetNext(charlie).SetNext(diana).SetNext(elmo).SetNext(fred);

            for (int i = 0; i < 500; i += 33)
            {
                alice.DoSupport(new Trouble(i));
            }
        }
    }
}