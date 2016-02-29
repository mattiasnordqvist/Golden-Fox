using System;

namespace GoldenFox.Internal.Operators.Intervals
{
    internal class Hour : Interval
    {
        public int OffsetInSeconds { get; set; }

        protected override DateTime ApplyRule(DateTime dateTime, bool inclusive)
        {
            var candidate = dateTime.StripMinutes().AddSeconds(OffsetInSeconds);
            if (inclusive && candidate == dateTime)
            {
                return candidate;
            }

            return candidate <= dateTime ? candidate.AddHours(1) : candidate;
        }
    }
}