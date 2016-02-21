using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.NewModel
{
    public class First 
    {
        private readonly List<IOperator<DateTime>> _nexts;

        public First(List<IOperator<DateTime>> nexts)
        {
            _nexts = nexts;
        }

        public DateTime Evaluate()
        {
            return _nexts.Select(x => x.Evaluate()).Min();
        }
    }
}