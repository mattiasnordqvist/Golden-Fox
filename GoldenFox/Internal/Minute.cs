using System;

namespace GoldenFox.Internal
{
    internal class Minute : Interval
    {
        private readonly Between _between;

        public Minute()
        {
            
        }

        public Minute(Between between)
        {
            _between = between;
        }

        public override DateTime Evaluate(DateTime dateTime, bool includeNow = false)
        {
            DateTime candidate;
            if (includeNow && dateTime.Second == 0 && dateTime.Millisecond == 0)
            {
                candidate = dateTime;
            }

            else
            {
                candidate = dateTime.StripSeconds().AddMinutes(1);
            }

            while (_between != null && !_between.Contains(new Timestamp(candidate)))
            {
                candidate = candidate.AddMinutes(1);
            }

            return candidate;

        }
    }
}