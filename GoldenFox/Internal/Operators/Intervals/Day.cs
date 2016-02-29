using System;

namespace GoldenFox.Internal.Operators.Intervals
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

        protected override DateTime ApplyRule(DateTime dateTime, bool inclusive)
        {
            var comparison = _timestamp.CompareTo(dateTime);
            if (comparison == 0 && inclusive)
            {
                return dateTime;
            }
            else
            {
                var daysToAdd = comparison <= 0 ? 1 : 0;
                return dateTime
                    .AddDays(daysToAdd)
                    .SetTime(_timestamp);
            }
        }
    }
}