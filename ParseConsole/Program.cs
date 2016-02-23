using System;
using System.IO;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

using TestSomething;

namespace ParseConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var line = Console.ReadLine();
                line = "every day @ 16:15 and every tuesday at 15:16";
                Console.WriteLine(line);
                var lexer = new GoldenFoxLanguageLexer(new AntlrInputStream(new StringReader(line)));
                var tokens = new CommonTokenStream(lexer);
                var parser = new GoldenFoxLanguageParser(tokens);
                var tree = parser.schedule();
                var visitor = new Visitor();
                var schedule = visitor.Visit(tree);
                Console.WriteLine(schedule.Evaluate(DateTime.Now));
            }
        }
    }
}