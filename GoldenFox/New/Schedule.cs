using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.New
{
    public class Schedule
    {
        private List<Clock> _times;

        private List<int> _daysOffset;

        public Schedule(Clock times, int days, Interval interval) : this(new List<Clock> { times }, new List<int> { days }, interval)
        {
        }

        public Schedule(List<Clock> times, int days, Interval interval) : this(times, new List<int> { days }, interval)
        {
        }

        public Schedule(Clock times, List<int> days, Interval interval) : this(new List<Clock> { times }, days, interval)
        {
        }

        public Schedule(List<Clock> times, List<int> days, Interval interval)
        {
            Times = times;
            DaysOffset = days;
            Interval = interval;
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

        public List<int> DaysOffset
        {
            get
            {
                return _daysOffset;
            }

            set
            {
                _daysOffset = value.OrderBy(x => x).ToList();
            }
        }

        public Interval Interval { get; set; }

        public DateTime Next(DateTime from, bool includeNow = false)
        {
            if (Interval == Interval.Day || Interval == Interval.Week)
            {
                return DaysOffset.Select(
                    day =>
                    {
                        var daysToNextOccurence = GetDaysToNextOccurence(from, Interval, day);

                        return Times.Select(
                            time => (includeNow && daysToNextOccurence % GetIntervalLength(Interval) == 0 && time.CompareTo(new Clock(@from)) == 0)
                                        ? @from
                                        : (time.CompareTo(new Clock(@from)) > 0 && daysToNextOccurence % GetIntervalLength(Interval) == 0
                                               ? @from.At(time) 
                                               : @from.AddDays(daysToNextOccurence).At(time))).OrderBy(x => x).First();
                    }).OrderBy(x => x).First();
            }

            return from;
        }

        private int GetDaysToNextOccurence(DateTime @from, Interval interval, int daysOffset)
        {
            return interval == Interval.Day
                ? GetIntervalLength(interval)
                : daysOffset <= ((int)from.DayOfWeek - 1)
                    ? GetIntervalLength(interval) - ((int)from.DayOfWeek - 1) + daysOffset
                    : daysOffset - ((int)from.DayOfWeek - 1);
        }

        private int GetIntervalLength(Interval interval)
        {
            return interval == Interval.Day ? 1 : 7;
        }
    }
}
