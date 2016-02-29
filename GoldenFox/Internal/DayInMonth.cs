using System;

namespace GoldenFox.Internal
{
    internal class DayInMonth : Interval
    {
        private readonly int _day;

        private readonly Timestamp _timestamp;

        private Func<DateTime, DateTime> _stepFunc;

        public DayInMonth(int day, Timestamp timestamp)
        {
            _day = day;
            _timestamp = timestamp;
        }

        protected override DateTime ApplyRule(DateTime dateTime, bool inclusive)
        {
            var comparison = new Timestamp(dateTime).CompareTo(_timestamp);
            DateTime candidate;
            if (IsSameDay(dateTime, _day))
            {
                if (comparison == 0 && inclusive)
                {
                    _stepFunc = x => x;
                    return _stepFunc(dateTime);
                }

                if (comparison > 0 || (comparison == 0 && !inclusive))
                {
                    _stepFunc = x => x.AddMonths(1).SetDayInMonth(_day).SetTime(_timestamp);
                    candidate = _stepFunc(dateTime);
                }
                else
                {
                    _stepFunc = x => x.SetTime(_timestamp);
                    candidate = _stepFunc(dateTime);
                }
            }
            else
            {
                if (_day > 0)
                {
                    if (_day > dateTime.Day)
                    {
                        _stepFunc = x => x.SetDayInMonth(_day).SetTime(_timestamp);
                        candidate = _stepFunc(dateTime);
                    }
                    else
                    {
                        _stepFunc = x => x.AddMonths(1).SetDayInMonth(_day).SetTime(_timestamp);
                        candidate = _stepFunc(dateTime);
                    }
                }
                else
                {
                    if (dateTime.DaysOfMonth() + _day > dateTime.Day)
                    {
                        _stepFunc = x => x.SetDayInMonth(_day).SetTime(_timestamp);
                        candidate = _stepFunc(dateTime);
                    }
                    else
                    {
                        _stepFunc = x => x.AddMonths(1).SetDayInMonth(_day).SetTime(_timestamp);
                        candidate = _stepFunc(dateTime);
                    }
                }
            }
            return candidate;
        }

        private DateTime Step(DateTime candidate)
        {
            return _stepFunc(candidate);
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
