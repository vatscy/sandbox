using System;

namespace Vatscy.DesignPattern.ChainOfResponsibility
{
    public abstract class Support
    {
        private string _name;

        private Support _next;

        public Support(string name)
        {
            _name = name;
        }

        public virtual Support SetNext(Support next)
        {
            _next = next;
            return next;
        }

        public void DoSupport(Trouble trouble)
        {
            if (Resolve(trouble))
            {
                Done(trouble);
            }
            else if (_next != null)
            {
                _next.DoSupport(trouble);
            }
            else
            {
                Fail(trouble);
            }
        }

        public override string ToString()
        {
            return "[" + _name + "]";
        }

        protected abstract bool Resolve(Trouble trouble);

        protected virtual void Done(Trouble trouble)
        {
            Console.WriteLine(trouble + " is resolved by " + this + ".");
        }

        protected virtual void Fail(Trouble trouble)
        {
            Console.WriteLine(trouble + " cannot be resolved.");
        }
    }
}