using System;

namespace GoldenFox.NewModel
{
    public class DayInMonth : Interval
    {
        private readonly int _day;

        private readonly Timestamp _timestamp;

        public DayInMonth(int day, Timestamp timestamp)
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
                if (comparison > 0 || comparison == 0 && !includeNow)
                {
                    return from.AddMonths(1).SetDay(_day).SetTime(_timestamp);
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
                    if (_day > @from.Day)
                    {
                        return @from.SetDay(_day).SetTime(_timestamp);
                    }
                    else
                    {
                        return @from.AddMonths(1).SetDay(_day).SetTime(_timestamp);
                    }
                }
                else
                {
                    if(@from.DaysOfMonth() + _day > @from.Day)
                    {
                        return @from.SetDay(_day).SetTime(_timestamp);
                    }
                    else
                    {
                        return @from.AddMonths(1).SetDay(_day).SetTime(_timestamp);
                    }
                }
            }
            
        }

        private bool IsSameDay(DateTime @from, int day)
        {
            if (day > 0 && day == @from.Day)
            {
                return true;
            }
            if (day <= 0 && @from.Day - @from.DaysOfMonth()  == day)
            {
                return true;
            }
            return false;
        }
    }
}
