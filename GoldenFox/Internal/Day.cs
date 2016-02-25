using System;

namespace GoldenFox.Internal
{
    internal class Day : Interval
    {
        private readonly Timestamp _timestamp;

        public Day() : this(new Timestamp())
        {
        }

        public Day(Timestamp timestamp)
        {
            _timestamp = timestamp;
        }

        public override DateTime Evaluate(DateTime from, bool includeNow = false)
        {
            var comparison = _timestamp.CompareTo(from);
            if (comparison == 0 && includeNow)
            {
                return from;
            }

            var daysToAdd = comparison <= 0 ? 1 : 0;
            return from
                .AddDays(daysToAdd)
                .SetTime(_timestamp);
        }
    }
}