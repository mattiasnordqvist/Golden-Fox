using System;

namespace GoldenFox
{
    public class ParsingException : Exception
    {
        public ParsingException(string message) : base(message)
        {
        }
    }
}