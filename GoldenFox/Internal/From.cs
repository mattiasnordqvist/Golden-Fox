using System;

namespace GoldenFox.Internal
{
    internal class From : IConstraint
    {
        private readonly DateTime _from;

        public From(DateTime from)
        {
            _from = @from;
        }

        public ConstraintResult Contains(DateTime dateTime, bool includeNow)
        {
            var passed = includeNow ?  _from <= dateTime : _from < dateTime;
            return new ConstraintResult { Passed = passed, ClosestValidFutureInput = passed ? dateTime : _from, };
        }
    }
}