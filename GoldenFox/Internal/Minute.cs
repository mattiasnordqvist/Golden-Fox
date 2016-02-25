using System;

namespace GoldenFox.Internal
{
    internal class Minute : Interval
    {
        public Between Between { get; set; }

        public int OffsetInSeconds { get; set; }

        public override DateTime Evaluate(DateTime dateTime, bool includeNow = false)
        {
            DateTime candidate;
            if (includeNow && dateTime.Second == OffsetInSeconds && dateTime.Millisecond == 0)
            {
                candidate = dateTime;
            }
            else
            {
                candidate = dateTime.StripSeconds().AddSeconds(OffsetInSeconds);
                if (dateTime.Second > OffsetInSeconds
                    || (dateTime.Second == OffsetInSeconds && !includeNow))
                {
                    candidate = candidate.AddMinutes(1);
                }
            }

            while (Between != null && !Between.Contains(new Timestamp(candidate)))
            {
                candidate = candidate.AddMinutes(1);
            }

            return candidate;
        }
    }
}