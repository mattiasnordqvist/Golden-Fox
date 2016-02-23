using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.NewModel
{
    public class First  : IOperator<DateTime>
    {
        private readonly List<IOperator<DateTime>> _nexts;

        public First(List<IOperator<DateTime>> nexts)
        {
            _nexts = nexts;
        }

        public DateTime Evaluate(DateTime from, bool includeNow = false)
        {
            return _nexts.Select(x => x.Evaluate(from, includeNow)).Min();
        }
    }
}