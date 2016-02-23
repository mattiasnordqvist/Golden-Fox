using System;

namespace GoldenFox.NewModel
{
    public class Weekday : Interval
    {
        private readonly DayOfWeek _day;

        private readonly Timestamp _timestamp;

        public Weekday(DayOfWeek day, Timestamp timestamp)
        {
            _day = day;
            _timestamp = timestamp;
        }

        public override DateTime Evaluate(DateTime from, bool includeNow = false)
        {
            var comparison = _timestamp.CompareTo(from);

            if (includeNow && from.DayOfWeek == _day && comparison == 0)
            {
                return from;
            }
            else
            {
                int daysToAdd;
                if (comparison >= 0 && _day == @from.DayOfWeek)
                {
                    if (comparison == 0 && includeNow)
                    {
                        // Same day, just later
                        daysToAdd = 0;
                    }
                    else if (comparison == 0 && !includeNow)
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