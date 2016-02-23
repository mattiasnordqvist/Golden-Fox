using System;

namespace GoldenFox.NewModel
{
    public class Constant : IOperator<DateTime>
    {
        private readonly DateTime _constant;

        public Constant(DateTime constant)
        {
            _constant = constant;
        }

        public DateTime Evaluate(DateTime from, bool includeNow)
        {
            return _constant;
        }
    }
}