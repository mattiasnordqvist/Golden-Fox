using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox
{
    public class Schedule
    {
        private List<Clock> _times;

        private List<Tuple<int, Interval>> _days;

        public Schedule()
        {
            _times = new List<Clock>();
            _days = new List<Tuple<int, Interval>>();
        }

        public Schedule(Clock times, int days, Interval interval) : this(new List<Clock> { times }, new List<int> { days }, interval)
        {
        }

        public Schedule(List<Clock> times, int days, Interval interval)
            : this(times, new List<int> { days }, interval)
        {
        }

        public Schedule(Clock times, IEnumerable<int> days, Interval interval)
            : this(new List<Clock> { times }, days, interval)
        {
        }

        public Schedule(List<Clock> times, IEnumerable<int> days, Interval interval)
        {
            Times = times;
            Days = days.Select(x => Tuple.Create(x, interval)).ToList();
        }

        public List<Clock> Times
        {
            get
            {
                return _times;
            }

            set
            {
                _times = value.OrderBy(x => x).ToList();
            }
        }

        public List<Tuple<int, Interval>> Days
        {
            get
            {
                return _days;
            }

            set
            {
                _days = value.OrderBy(x => x.Item1).ToList();
            }
        }

        public static GoldenFoxFactory Fox(string schedule)
        {
            return new GoldenFoxFactory(schedule);
        }

        public DateTime Next(DateTime from, bool includeNow = false)
        {
                return Days.Select(
                    day =>
                    {
                        var daysToNextOccurence = GetDaysToNextOccurence(from, day.Item2, day.Item1);

                        return Times.Select(
                            time => (includeNow && daysToNextOccurence % GetIntervalLength(day.Item2, from) == 0 && time.CompareTo(new Clock(@from)) == 0)
                                        ? @from
                                        : (time.CompareTo(new Clock(@from)) > 0 && daysToNextOccurence % GetIntervalLength(day.Item2, from) == 0
                                               ? @from.At(time) 
                                               : @from.AddDays(daysToNextOccurence).At(time))).OrderBy(x => x).First();
                    }).OrderBy(x => x).First();
        }

        private int GetDaysToNextOccurence(DateTime from, Interval interval, int daysOffset)
        {
            var nextDayIfThisInterval = (daysOffset >= 0 ? 0 : GetIntervalLength(interval, from)) + 1 + daysOffset;
            if (nextDayIfThisInterval > DayIn(from, interval))
            {
                return nextDayIfThisInterval - DayIn(from, interval);
            }
            else
            {
                var nextDayIfNextInterval = (daysOffset >= 0 ? 0 : GetIntervalLength(interval, Add(from, interval))) + 1 + daysOffset;
                return GetIntervalLength(interval, from) - DayIn(from, interval) + nextDayIfNextInterval;
            }
        }

        private int DayIn(DateTime @from, Interval interval)
        {
            switch (interval)
            {
                case Interval.Week:
                    return ((int)@from.DayOfWeek == 0) ? 7 : (int)@from.DayOfWeek;
                case Interval.Month:
                    return @from.Day;
                case Interval.Year:
                    return @from.DayOfYear;
                default: throw new Exception();
            }
        }

        private DateTime Add(DateTime @from, Interval interval)
        {
            switch (interval)
            {
                case Interval.Week:
                    return @from.AddDays(7);
                case Interval.Month:
                    return @from.AddMonths(1);
                case Interval.Year:
                    return @from.AddYears(1);
                default:
                    throw new Exception();
            }
        }

        private int GetIntervalLength(Interval interval, DateTime context)
        {
            switch (interval)
            {
                case Interval.Week:
                    return 7;
                case Interval.Month:
                    return DateTime.DaysInMonth(context.Year, context.Month);
                case Interval.Year:
                    return new DateTime(context.Year, 12, 31).DayOfYear;
                default:
                    throw new Exception();
            }
        }
    }
}
