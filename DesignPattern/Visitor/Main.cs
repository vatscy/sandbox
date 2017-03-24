using System;
using Vatscy.DesignPattern.Command;

namespace Vatscy.DesignPattern.Visitor
{
    public class Main : PatternCommand
    {
        public Main() : base("Visitor") { }

        public override void Execute()
        {
            try
            {
                Console.WriteLine("Making root entries...");
                var rootdir = new Directory("root");
                var bindir = new Directory("bin");
                var tmpdir = new Directory("tmp");
                var usrdir = new Directory("usr");
                rootdir.Add(bindir);
                rootdir.Add(tmpdir);
                rootdir.Add(usrdir);
                bindir.Add(new File("vi", 10000));
                bindir.Add(new File("latex", 20000));
                rootdir.Accept(new ListVisitor());

                Console.WriteLine();
                Console.WriteLine("Making user entries...");
                var yuki = new Directory("yuki");
                var hanako = new Directory("hanako");
                var tomura = new Directory("tomura");
                usrdir.Add(yuki);
                usrdir.Add(hanako);
                usrdir.Add(tomura);
                yuki.Add(new File("diary.html", 100));
                yuki.Add(new File("Composite.java", 200));
                hanako.Add(new File("memo.tex", 300));
                tomura.Add(new File("game.doc", 400));
                tomura.Add(new File("junk.mail", 500));
                rootdir.Accept(new ListVisitor());
            }
            catch (FileTreatmentException e)
            {
                Console.Error.WriteLine(e.StackTrace);
            }
        }
    }
}