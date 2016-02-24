using System;
using System.Linq;

namespace TestConsole
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