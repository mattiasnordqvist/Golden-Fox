using System;

namespace GoldenFox.NewModel
{
    public class Day : Interval
    {
        private readonly Timestamp _timestamp;

        public Day(From from, bool includeNow = false) : this(new Timestamp(),from, includeNow)
        {
        }

        public Day(Timestamp timestamp, From from, bool includeNow = false)
            : base(from, includeNow)
        {
            _timestamp = timestamp;
        }

        public override DateTime Evaluate()
        {
            var from = From.Evaluate();
            var comparison = _timestamp.CompareTo(from);
            if (comparison == 0 && IncludeNow)
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