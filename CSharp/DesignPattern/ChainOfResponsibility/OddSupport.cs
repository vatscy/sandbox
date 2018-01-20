namespace Vatscy.DesignPattern.ChainOfResponsibility
{
    public class OddSupport : Support
    {
        public OddSupport(string name) : base(name) { }

        protected override bool Resolve(Trouble trouble)
        {
            return trouble.Number % 2 == 1;
        }
    }
}