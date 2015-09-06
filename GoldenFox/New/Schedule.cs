using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.New
{
    public class Schedule
    {
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

        public List<Clock> Times { get; set; } 
        public List<int> DaysOffset { get; set; }
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
                                times =>
                                    {
                                        // The 2 first ifs are what is causing tests to fail right now.
                                        if (times.CompareTo(new Clock(from)) > 0)
                                        {
                                            return from.At(times);
                                        }

                                        if (times.CompareTo(new Clock(@from)) < 0)
                                        {
                                            return @from.AddDays(daysToNextOccurence).At(times);
                                        }

                                        return (includeNow && daysToNextOccurence % GetIntervalLength(Interval) == 0)
                                                   ? @from
                                                   : @from.AddDays(daysToNextOccurence).At(times);
                                    }).OrderBy(x => x).First();
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
