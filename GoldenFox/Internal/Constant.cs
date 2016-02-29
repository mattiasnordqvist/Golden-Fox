using System;

namespace GoldenFox.Internal
{
    internal class Constant : IOperator<DateTime>
    {
        private readonly DateTime _constant;

        public Constant(DateTime constant)
        {
            _constant = constant;
        }

        public DateTime Evaluate(DateTime from, bool inclusive)
        {
            return _constant;
        }
    }
}