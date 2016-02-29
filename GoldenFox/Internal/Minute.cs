using System;

namespace GoldenFox.Internal
{
    internal class Minute : Interval
    {
        public int OffsetInSeconds { get; set; }


        protected override DateTime ApplyRule(DateTime dateTime, bool inclusive)
        {
            var candidate = dateTime.StripSeconds().AddSeconds(OffsetInSeconds);
            if (inclusive && candidate == dateTime)
            {
                return candidate;
            }

            return candidate <= dateTime ? candidate.AddMinutes(1) : candidate;
        }
    }
}