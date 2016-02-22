using System;

namespace GoldenFox.NewModel
{
    public class Weekday : Interval
    {
        private readonly DayOfWeek _day;

        private readonly Timestamp _timestamp;

        public Weekday(DayOfWeek day, Timestamp timestamp, From from, bool includeNow = false)
            : base(from, includeNow)
        {
            _day = day;
            _timestamp = timestamp;
        }

        public override DateTime Evaluate()
        {
            var from = From.Evaluate();
            var comparison = _timestamp.CompareTo(from);

            if (IncludeNow && from.DayOfWeek == _day && comparison == 0)
            {
                return from;
            }
            else
            {
                int daysToAdd;
                if (comparison >= 0 && _day == @from.DayOfWeek)
                {
                    if (comparison == 0 && IncludeNow)
                    {
                        // Same day, just later
                        daysToAdd = 0;
                    }
                    else if (comparison == 0 && !IncludeNow)
                    {
                        daysToAdd = 7;
                    }
                    else
                    {
                        daysToAdd = 0;
                    }
                }
                else
                {
                    // next occurence of weekday
                    var dayDiff = ((int)_day - (int)@from.DayOfWeek + 7) % 7;
                    daysToAdd = dayDiff == 0 ? 7 : dayDiff;
                }
                return from
                    .AddDays(daysToAdd)
                    .SetTime(_timestamp);
            }
        }
    }
}