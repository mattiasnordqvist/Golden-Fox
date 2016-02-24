using System;

namespace GoldenFox.Internal
{
    internal abstract class Interval : IOperator<DateTime>
    {
        public abstract DateTime Evaluate(DateTime dateTime, bool includeNow = false);
    }
}