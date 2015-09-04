using System;

namespace GoldenFox.Parsing
{
    public class ParsingException : Exception
    {
        public ParsingException(string message) : base(message)
        {
        }
    }
}