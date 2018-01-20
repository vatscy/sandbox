namespace Vatscy.DesignPattern.Visitor
{
    public class File : Entry
    {
        private string _name;

        public override string Name
        {
            get
            {
                return _name;
            }
        }

        private int _size;

        public override int Size
        {
            get
            {
                return _size;
            }
        }

        public File(string name, int size)
        {
            _name = name;
            _size = size;
        }

        public override void Accept(Visitor v)
        {
            v.Visit(this);
        }
    }
}