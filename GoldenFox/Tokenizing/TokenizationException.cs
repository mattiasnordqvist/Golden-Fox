using System;

namespace GoldenFox.Tokenizing
{
    public class TokenizationException : Exception
    {
        public TokenizationException(string message) : base("Unexpected: " + message)
        {
        }
    }
}