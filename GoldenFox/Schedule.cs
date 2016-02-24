using System;

namespace GoldenFox
{
    public class Schedule
    {

        private readonly string _schedule;

        internal Schedule(string schedule)
        {
            _schedule = schedule;
        }

        public Fox From(DateTime from)
        {
            return new Fox(_schedule, from);
        }
        public static Schedule Fox(string schedule)
        {
            return new Schedule(schedule);
        }
    }
}
