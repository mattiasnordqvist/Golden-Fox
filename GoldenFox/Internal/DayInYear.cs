using System;

namespace GoldenFox.Internal
{
    internal class DayInYear : Interval
    {
        private readonly int _day;

        private readonly Timestamp _timestamp;

        private Func<DateTime, DateTime> _stepFunc;

        public DayInYear(int day, Timestamp timestamp)
        {
            _day = day;
            _timestamp = timestamp;
        }
        
        protected override DateTime ApplyRule(DateTime datetime, bool includeNow)
        {
            var comparison = new Timestamp(datetime).CompareTo(_timestamp);

            DateTime candidate;
            if (IsSameDay(datetime, _day))
            {
                if (comparison == 0 && includeNow)
                {
                    _stepFunc = x => x;
                    return _stepFunc(datetime);
                }

                if (comparison > 0 || (comparison == 0 && !includeNow))
                {
                    _stepFunc = x => x.AddYears(1).SetDayInYear(_day).SetTime(_timestamp);
                    candidate = _stepFunc(datetime);
                }
                else
                {
                    _stepFunc = x => x.SetTime(_timestamp);
                    candidate = _stepFunc(datetime);
                }
            }
            else
            {
                if (_day > 0)
                {
                    if (_day > datetime.DayOfYear)
                    {
                        _stepFunc = x => x.SetDayInYear(_day).SetTime(_timestamp);
                        candidate = _stepFunc(datetime);
                    }
                    else
                    {
                        _stepFunc = x => x.AddYears(1).SetDayInYear(_day).SetTime(_timestamp);
                        candidate = _stepFunc(datetime);
                    }
                }
                else
                {
                    if (datetime.DaysOfMonth() + _day > datetime.Day)
                    {
                        _stepFunc = x => x.SetDayInYear(_day).SetTime(_timestamp);
                        candidate = _stepFunc(datetime);
                    }
                    else
                    {
                        _stepFunc = x => x.AddYears(1).SetDayInYear(_day).SetTime(_timestamp);
                        candidate = _stepFunc(datetime);
                    }
                }
            }

            return candidate;
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
