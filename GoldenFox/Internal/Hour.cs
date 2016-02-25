using System;

namespace GoldenFox.Internal
{
    internal class Hour : Interval
    {
        public Between Between { get; set; }

        public int OffsetInSeconds { get; set; }

        private int OffMinutes => (OffsetInSeconds - (OffsetInSeconds % 60)) / 60;

        private int OffSeconds => OffsetInSeconds - (OffMinutes * 60);

        public override DateTime Evaluate(DateTime dateTime, bool includeNow = false)
        {
            DateTime candidate;
            if (includeNow && dateTime.Minute == OffMinutes && dateTime.Second == OffSeconds && dateTime.Millisecond == 0)
            {
                candidate = dateTime;
            }
            else
            {
                candidate = dateTime.StripMinutes().AddSeconds(OffsetInSeconds);
                if (dateTime.Minute > OffMinutes 
                    || (dateTime.Minute == OffMinutes && dateTime.Second > OffSeconds) 
                    || (dateTime.Minute == OffMinutes && dateTime.Second == OffSeconds && !includeNow))
                {
                    candidate = candidate.AddHours(1);
                }
            }

            while (Between != null && !Between.Contains(new Timestamp(candidate)))
            {
                candidate = candidate.AddHours(1);
            }

            return candidate;
        }
    }
}