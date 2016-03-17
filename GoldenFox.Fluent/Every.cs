using System;
using System.Collections.Generic;
using System.Linq;

using GoldenFox.Internal.Constraints;
using GoldenFox.Internal.Operators;

namespace GoldenFox.Fluent
{

    public abstract class Every : IOperator
    {
        private readonly List<IConstraint> _constraints = new List<IConstraint>();

        protected Every()
        {
            _operators.Add(this);
        }

        internal IOperator Build()
        {
            var operatorBuilder = InternalBuild();
            var op = operatorBuilder.Build(_constraints);
            return op;
        }

        internal abstract OperatorBuilder InternalBuild();

        public static HourBuilder Hour()
        {
            var hourBuilder = new HourBuilder();
            return hourBuilder;
        }

        public static MinuteBuilder Minute()
        {
            var minuteBuilder = new MinuteBuilder();
            return minuteBuilder;
        }

        public static SecondBuilder Second()
        {
            var minuteBuilder = new SecondBuilder();
            return minuteBuilder;
        }

        private readonly List<Every> _operators = new List<Every>();


        public Every And(Every every)
        {
            _operators.Add(every);
            return this;
        }

        internal void AddConstraint(IConstraint contraint)
        {
            _constraints.Add(contraint);
            foreach (var @operator in _operators)
            {
                if (@operator != this)
                {
                    @operator.AddConstraint(contraint);
                }
            }
            
        }

        public DateTime Evaluate(DateTime @from, bool inclusive = false)
        {
            return new First(_operators.Select(x => x.Build()).ToList()).Evaluate(@from, inclusive);
        }
    }
}