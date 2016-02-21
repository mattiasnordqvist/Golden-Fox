using System;

namespace GoldenFox.NewModel
{
    public class Weekday : Interval
    {
        private readonly DayOfWeek _day;

        private Timestamp _timestamp;

        public Weekday(DayOfWeek day, Timestamp timestamp, From from, bool includeNow = false)
            : base(from, includeNow)
        {
            _day = day;
            _timestamp = timestamp;
        }

        public override DateTime Evaluate()
        {
            var from = From.Evaluate();
            if (IncludeNow && from.DayOfWeek == _day && _timestamp.CompareTo(from) == 0)
            {
                return from;
            }
            else
            {
                var comparison = _timestamp.CompareTo(from);
                var daysToAdd = comparison <= 0 ? ((_day - from.DayOfWeek + 7) % 7) : 0;
                return from
                    .AddDays(daysToAdd)
                    .SetTime(_timestamp);
                return from;
            }
        }
    }
}