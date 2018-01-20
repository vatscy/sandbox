using System.Collections.Generic;
using System.Linq;

namespace Vatscy.DesignPattern.Visitor
{
    public class Directory : Entry, IEnumerable<Entry>
    {
        private string _name;

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        public override int Size
        {
            get
            {
                return _dir.Aggregate(0, (a, entry) => a + entry.Size);
            }
        }

        private List<Entry> _dir = new List<Entry>();

        public Directory(string name)
        {
            _name = name;
        }

        public override Entry Add(Entry entry)
        {
            _dir.Add(entry);
            return this;
        }

        public override void Accept(Visitor v)
        {
            v.Visit(this);
        }

        public IEnumerator<Entry> GetEnumerator()
        {
            return _dir.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}