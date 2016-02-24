using System;

namespace GoldenFox
{
    public class Schedule
    {
        public static GoldenFoxFactory Fox(string schedule)
        {
            return new GoldenFoxFactory(schedule);
        }
    }
}
