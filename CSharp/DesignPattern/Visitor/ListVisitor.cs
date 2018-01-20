using System;
using System.Linq;

namespace Vatscy.DesignPattern.Visitor
{
    public class ListVisitor : Visitor
    {
        private string _currentDir = "";

        public override void Visit(File file)
        {
            Console.WriteLine(_currentDir + "/" + file);
        }

        public override void Visit(Directory dir)
        {
            Console.WriteLine(_currentDir + "/" + dir);
            var saveDir = _currentDir;
            _currentDir = _currentDir + "/" + dir.Name;
            dir.ForEach(e => e.Accept(this));
            _currentDir = saveDir;
        }
    }
}