namespace Vatscy.DesignPattern.Visitor
{
    public abstract class Entry : IElement
    {
        public abstract string Name { get; }

        public abstract int Size { get; }

        public virtual Entry Add(Entry entry)
        {
            throw new FileTreatmentException();
        }

        public abstract void Accept(Visitor v);

        public override string ToString()
        {
            return Name + "(" + Size + ")";
        }
    }
}