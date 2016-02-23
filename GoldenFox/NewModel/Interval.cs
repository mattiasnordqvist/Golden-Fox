using System;

namespace GoldenFox.NewModel
{
    public abstract class Interval : IOperator<DateTime>
    {
        public abstract DateTime Evaluate(DateTime dateTime, bool includeNow = false);
    }
}