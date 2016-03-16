using System;

namespace GoldenFox.Internal.Constraints
{
    public class ConstraintResult
    {
        public bool Passed { get; set; }

        public DateTime ClosestValidFutureInput { get; set; }
    }
}