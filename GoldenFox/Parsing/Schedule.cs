using System;
using System.Collections.Generic;

namespace GoldenFox.Parsing
{
    public class Schedule
    {
        public Schedule(string s)
        {
            Parts = new ScheduleParser().ParseParts(s.ToLower());
        }

        public Schedule(List<string> parts)
        {
            Parts = parts;
        }

        public List<string> Parts { get; private set; }

        public static GoldenFoxFactory Fox(string schedule)
        {
            return new GoldenFoxFactory(schedule);
        }

        public DateTime Next(DateTime given)
        {
            return new DateTimeComputer(Parts).Next(given);
        }
    }
}