using System;

namespace GoldenFox.Internal.Constraints
{
    internal class ConstraintResult
    {
        public bool Passed { get; set; }

        public DateTime ClosestValidFutureInput { get; set; }
    }
}