using System;

namespace GoldenFox.Internal
{
    internal class Hour : Interval
    {
        private readonly Between _between;

        public Hour()
        {
            
        }

        public Hour(Between between)
        {
            _between = between;
        }

        public override DateTime Evaluate(DateTime dateTime, bool includeNow = false)
        {
            DateTime candidate;
            if (includeNow && dateTime.Minute == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0)
            {
                candidate = dateTime;
            }
            else
            { 
                candidate = dateTime.StripMinutes().AddHours(1);
            }
            while (_between != null && !_between.Contains(new Timestamp(candidate)))
            {
                candidate = candidate.AddHours(1);
            }

            return candidate;
        }
    }
}