using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.New
{
    public class Schedule
    {
        public List<Clock> AtTimes { get; set; } 
        public int DaysOffset { get; set; }
        public Interval Interval { get; set; }

        public DateTime Next(DateTime from, bool orNow = false)
        {
            if (Interval == Interval.Day)
            {
                return AtTimes.Select(
                    t =>
                        {
                            if (t.CompareTo(new Clock(from)) > 0)
                            {
                                return from.Date.AddHours(t.Hour).AddMinutes(t.Minute).AddSeconds(t.Seconds);
                            }
                            else if (t.CompareTo(new Clock(from)) < 0)
                            {
                                return from.Date.AddDays(1).AddHours(t.Hour).AddMinutes(t.Minute).AddSeconds(t.Seconds);
                            }
                            else
                            {
                                return orNow
                                           ? from
                                           : from.Date.AddDays(1).AddHours(t.Hour).AddMinutes(t.Minute).AddSeconds(t.Seconds);
                            }
                        }).OrderBy(x => x).First();
            }
            return from;
        }
    }
}
