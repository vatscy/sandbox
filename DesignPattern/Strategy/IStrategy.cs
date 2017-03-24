namespace Vatscy.DesignPattern.Strategy
{
    public interface IStrategy
    {
        Hand NextHand();
        void Study(bool win);
    }
}