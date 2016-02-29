using System;

namespace GoldenFox.Internal
{
    internal class Second : Interval
    {
        protected override DateTime ApplyRule(DateTime dateTime, bool inclusive)
        {
            var candidate = dateTime.StripMilliseconds();
            if (inclusive && candidate == dateTime)
            {
                return candidate;
            }

            return candidate <= dateTime ? candidate.AddSeconds(1) : candidate;
        }
    }
}