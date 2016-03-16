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


                //var result = new GoldenFox.Fox(line, DateTime.Now).First();
                var result = Every.Minute().WithOffset(30).Between("00:10").And("00:20")
                    .And(Every.Minute().WithOffset(45)).Evaluate(DateTime.Now);
                Console.WriteLine(result);
               
                //new Day().Between(new Timestamp()).And(new Timestamp()).Until(DateTime.Today);
                    //;
            }
        }
    }
}