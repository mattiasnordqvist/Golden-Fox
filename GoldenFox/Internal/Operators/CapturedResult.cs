using System;

namespace GoldenFox.Internal.Operators
{
    internal class CapturedResult
    {
        public Exception Exception { get; set; }
        public DateTime? DateTime { get; set; }
    }
}