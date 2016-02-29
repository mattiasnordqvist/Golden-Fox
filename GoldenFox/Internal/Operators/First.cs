using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.Internal.Operators
{
    internal class First : IOperator<DateTime>
    {
        private readonly List<IOperator<DateTime>> _nexts;

        public First(IOperator<DateTime> o1, IOperator<DateTime> o2) : this(new List<IOperator<DateTime>> { o1, o2 })
        {
        }

        public First(List<IOperator<DateTime>> nexts)
        {
            _nexts = nexts;
        }

        public DateTime Evaluate(DateTime from, bool inclusive = false)
        {
            return _nexts.Select(x => x.Evaluate(from, inclusive)).Min();
        }
    }
}