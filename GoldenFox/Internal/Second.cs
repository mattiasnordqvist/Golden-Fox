using System;

namespace GoldenFox.Internal
{
    internal class Second : Interval
    {
        public Between Between { get; set; }

        public override DateTime Evaluate(DateTime dateTime, bool includeNow = false)
        {
            DateTime candidate;
            if (includeNow && dateTime.Second == 0 && dateTime.Millisecond == 0)
            {
                candidate = dateTime;
            }
            else
            {
                candidate = dateTime.StripMilliseconds().AddSeconds(1);
            }

            while (Between != null && !Between.Contains(new Timestamp(candidate)))
            {
                candidate = candidate.AddSeconds(1);
            }

            return candidate;
        }
    }
}