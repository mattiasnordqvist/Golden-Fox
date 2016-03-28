using System;

namespace GoldenFox.Internal.Constraints
{
    public class From : IConstraint
    {
        private readonly DateTime _from;

        public From(DateTime from)
        {
            _from = @from;
        }

        public ConstraintResult Contains(DateTime dateTime)
        {
            var passed = _from <= dateTime;
            return new ConstraintResult { Passed = passed, ClosestValidFutureInput = passed ? dateTime : _from, };
        }
    }
}