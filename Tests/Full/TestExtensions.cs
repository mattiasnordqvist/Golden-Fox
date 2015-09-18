using System;

using GoldenFox;

namespace Tests.Full
{
    public static class TestExtensions
    {
        public static TestableDateTime From(this string schedule, string from, bool includeNow = false)
        {
            return new TestableDateTime(DateTime.Parse(from), new ScheduleParser().Parse(schedule).Next(DateTime.Parse(from), includeNow: includeNow));
        }
    }
}