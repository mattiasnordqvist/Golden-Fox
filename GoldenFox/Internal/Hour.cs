using System;

namespace GoldenFox.Internal
{
    internal class Hour : Interval
    {
        public int OffsetInSeconds { get; set; }

        private int OffMinutes => (OffsetInSeconds - (OffsetInSeconds % 60)) / 60;

        private int OffSeconds => OffsetInSeconds - (OffMinutes * 60);

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