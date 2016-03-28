using System;
using System.Linq;

using GoldenFox;
using GoldenFox.Fluent;
using GoldenFox.Internal;
using GoldenFox.Internal.Operators.Intervals;

namespace TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                var line = Console.ReadLine();
                Console.WriteLine("Next occurence: ");
                var result = new Fox(line, DateTime.Now).First();
                Console.WriteLine(result);
            }
        }
    }
}