namespace Vatscy.DesignPattern.Strategy
{
    public class Player
    {
        private string _name;

        private IStrategy _strategy;

        private int _winCount;

        private int _loseCount;

        private int _gameCount;

        public Player(string name, IStrategy strategy)
        {
            _name = name;
            _strategy = strategy;
        }

        public virtual Hand NextHand()
        {
            return _strategy.NextHand();
        }

        public virtual void Win()
        {
            _strategy.Study(true);
            _winCount++;
            _gameCount++;
        }

        public virtual void Lose()
        {
            _strategy.Study(false);
            _loseCount++;
            _gameCount++;
        }

        public virtual void Even()
        {
            _gameCount++;
        }

        public override string ToString()
        {
            return "[" + _name + ":" + _gameCount + " games, " + _winCount + " win, " + _loseCount + " lose" + "]";
        }
    }
}