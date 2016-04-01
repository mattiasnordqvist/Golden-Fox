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
            var capturedResults = _nexts.Select(x => CaptureResult(() => x.Evaluate(from, inclusive))).ToList();
            if (capturedResults.Any(x => x.DateTime.HasValue))
            {
                return capturedResults.Where(x => x.DateTime.HasValue).Select(x => x.DateTime.Value).Min();
            }
            else
            {
                throw new InvalidOperationException(string.Empty, new AggregateException(capturedResults.Select(x => x.Exception)));
            }
        }

        private CapturedResult CaptureResult(Func<DateTime> func)
        {
            try
            {
                return new CapturedResult { DateTime = func() };
            }
            catch (InvalidOperationException e)
            {
                return new CapturedResult { Exception = e };
            }
        }
    }
}