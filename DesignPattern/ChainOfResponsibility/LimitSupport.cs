namespace Vatscy.DesignPattern.ChainOfResponsibility
{
    public class LimitSupport : Support
    {
        private int _limit;
		
        public LimitSupport(string name, int limit) : base(name)
        {
            _limit = limit;
        }

        protected override bool Resolve(Trouble trouble)
        {
            return trouble.Number < _limit;
        }
    }
}