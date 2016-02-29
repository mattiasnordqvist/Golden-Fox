using System;

namespace GoldenFox.Internal
{
    internal class Hour : Interval
    {
        public int OffsetInSeconds { get; set; }

        private int OffMinutes => (OffsetInSeconds - (OffsetInSeconds % 60)) / 60;

        private int OffSeconds => OffsetInSeconds - (OffMinutes * 60);

        protected override DateTime ApplyRule(DateTime dateTime, bool includeNow)
        {
            var candidate = dateTime.StripMinutes().AddSeconds(OffsetInSeconds);
            if (includeNow && candidate == dateTime)
            {
                return candidate;
            }
            return candidate <= dateTime ? candidate.AddHours(1) : candidate;
        }
    }
}