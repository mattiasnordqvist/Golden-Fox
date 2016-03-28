using System;
using System.Collections.Generic;

using GoldenFox.Internal.Constraints;

namespace GoldenFox.Fluent
{
    public class OperatorBuilder  
    {
        private readonly Func<List<IConstraint>, IOperator> _build;

        public OperatorBuilder(Func<List<IConstraint>, IOperator> build)
        {
            _build = build;
        }

        public IOperator Build(List<IConstraint> constraints)
        {
            return _build(constraints);
        }
    }
}