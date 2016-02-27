using System;

namespace GoldenFox.Internal
{
    internal class DayInYear : Interval
    {
        private readonly int _day;

        private readonly Timestamp _timestamp;

        public DayInYear(int day, Timestamp timestamp)
        {
            _day = day;
            _timestamp = timestamp;
        }

        public override DateTime Evaluate(DateTime from, bool includeNow = false)
        {
            var comparison = new Timestamp(from).CompareTo(_timestamp);
            
            if (IsSameDay(from, _day))
            {
                if (comparison == 0 && includeNow)
                {
                    return from;
                }

                if (comparison > 0 || (comparison == 0 && !includeNow))
                {
                    return from.AddYears(1).SetDayInYear(_day).SetTime(_timestamp);
                }
                else
                {
                    return from.SetTime(_timestamp);
                }
            }
            else
            {
                if (_day > 0)
                {
                    if (_day > @from.DayOfYear)
                    {
                        return @from.SetDayInYear(_day).SetTime(_timestamp);
                    }
                    else
                    {
                        return @from.AddYears(1).SetDayInYear(_day).SetTime(_timestamp);
                    }
                }
                else
                {
                    if (@from.DaysOfMonth() + _day > @from.Day)
                    {
                        return @from.SetDayInYear(_day).SetTime(_timestamp);
                    }
                    else
                    {
                        return @from.AddYears(1).SetDayInYear(_day).SetTime(_timestamp);
                    }
                }
            }
        }

        private bool IsSameDay(DateTime @from, int day)
        {
            if (day > 0 && day == @from.DayOfYear)
            {
                return true;
            }

            if (day <= 0 && @from.DayOfYear - @from.DaysOfYear() == day)
            {
                return true;
            }

            return false;
        }
    }
}
