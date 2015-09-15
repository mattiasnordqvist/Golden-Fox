using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.New
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

        private int GetDaysToNextOccurence(DateTime @from, Interval interval, int daysOffset)
        {
            var intervalLength = GetIntervalLength(interval, from);
            var current = interval == Interval.Week ? (int)from.DayOfWeek - 1 : from.Day - 1;
            var daysToNextOccurence = daysOffset <= current ? intervalLength - current + daysOffset : daysOffset - current;
            if (daysToNextOccurence <= 0)
            {
                daysToNextOccurence += intervalLength;
            }

            return daysToNextOccurence;
        }

        private int GetIntervalLength(Interval interval, DateTime context)
        {
            if (interval == Interval.Week)
            {
                return 7;
            }
            else 
            {
                // month
                return DateTime.DaysInMonth(context.Year, context.Month);
            }
        }
    }
}
