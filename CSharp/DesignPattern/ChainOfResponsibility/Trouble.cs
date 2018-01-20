namespace Vatscy.DesignPattern.ChainOfResponsibility
{
    public class Trouble
    {
        public virtual int Number { get; private set; }

        public Trouble(int number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return "[Trouble " + Number + "]";
        }
    }
}