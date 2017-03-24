namespace Vatscy.DesignPattern.Command
{
    public abstract class PatternCommand
    {
        public string PatternName { get; private set; }

        public PatternCommand(string patternName)
        {
            PatternName = patternName;
        }

        public abstract void Execute();
    }
}