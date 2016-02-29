using System;

namespace GoldenFox.Internal
{
    internal class ConstraintResult
    {
        public bool Passed { get; set; }

        public DateTime ClosestValidFutureInput { get; set; }
    }
}