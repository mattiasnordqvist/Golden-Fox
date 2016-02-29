using System;

using GoldenFox;

namespace Tests.Full
{
    public static class TestExtensions
    {
        public static TestableDateTime From(this string schedule, string from, bool inclusive = false)
        {
            return new TestableDateTime(DateTime.Parse(from), Fox.Compile(schedule).Evaluate(DateTime.Parse(from), inclusive));
        }
    }
}