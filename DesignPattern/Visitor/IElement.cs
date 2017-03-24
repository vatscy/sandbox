namespace Vatscy.DesignPattern.Visitor
{
    public interface IElement
    {
        void Accept(Visitor v);
    }
}