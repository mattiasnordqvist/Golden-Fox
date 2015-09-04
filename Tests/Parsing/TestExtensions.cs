using System;

using GoldenFox.Parsing;

namespace Tests.Parsing
{
    public static class TestExtensions
    {
        public static TestableDateTime From(this string schedule, string from)
        {
            return new TestableDateTime(DateTime.Parse(from), new Schedule(schedule).Next(DateTime.Parse(from)));
        }
    }
}