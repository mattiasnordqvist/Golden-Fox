using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.Internal.Operators
{
    internal class First : IOperator
    {
        private readonly List<IOperator> _nexts;

        public First(IOperator o1, IOperator o2) : this(new List<IOperator> { o1, o2 })
        {
        }

        public First(List<IOperator> nexts)
        {
            _nexts = nexts;
        }

        public DateTime Evaluate(DateTime from, bool inclusive = false)
        {
            return _nexts.Select(x => x.Evaluate(from, inclusive)).Min();
        }
    }
}