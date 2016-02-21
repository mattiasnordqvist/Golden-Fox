using System;

namespace GoldenFox.NewModel
{
    public abstract class Interval : IOperator<DateTime>
    {
        protected readonly bool IncludeNow;

        protected readonly From From;

        protected Interval(From from, bool includeNow)
        {
            IncludeNow = includeNow;
            From = @from;
        }

        public abstract DateTime Evaluate();
    }
}