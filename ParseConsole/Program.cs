using System;
using System.IO;
using System.Linq;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

using GoldenFox.NewModel;

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
                Console.WriteLine("Next occurence: ");
                var result = new GoldenFox.Fox(line, DateTime.Now).First();
                Console.WriteLine(result);
            }
        }
    }
}