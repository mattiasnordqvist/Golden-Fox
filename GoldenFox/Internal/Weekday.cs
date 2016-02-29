using System;

namespace GoldenFox.Internal
{
    internal class Weekday : Interval
    {
        private readonly DayOfWeek _day;

        private readonly Timestamp _timestamp;

        private int _tempDaysToAdd;

        public Weekday(DayOfWeek day, Timestamp timestamp)
        {
            _day = day;
            _timestamp = timestamp;
        }

        protected override DateTime ApplyRule(DateTime datetime, bool inclusive)
        {
            var comparison = _timestamp.CompareTo(datetime);
            DateTime candidate;
            if (inclusive && datetime.DayOfWeek == _day && comparison == 0)
            {
                candidate = datetime;
            }
            else
            {
                int daysToAdd;
                if (comparison >= 0 && _day == datetime.DayOfWeek)
                {
                    if (comparison == 0 && inclusive)
                    {
                        // Same day, just later
                        daysToAdd = 0;
                    }
                    else if (comparison == 0 && !inclusive)
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
                    var dayDiff = ((int)_day - (int)datetime.DayOfWeek + 7) % 7;
                    daysToAdd = dayDiff == 0 ? 7 : dayDiff;
                }
                _tempDaysToAdd = daysToAdd;
                candidate = datetime.AddDays(_tempDaysToAdd).SetTime(_timestamp);
            }
            return candidate;

        }

    }
}