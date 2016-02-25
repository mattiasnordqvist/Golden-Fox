using System;
using System.Linq;

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
                var result = new GoldenFox.Fox(line, DateTime.Now).First();
                Console.WriteLine(result);
            }
        }
    }
}