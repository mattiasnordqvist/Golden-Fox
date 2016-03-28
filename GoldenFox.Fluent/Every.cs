using System;
using System.Collections.Generic;
using System.Linq;

using GoldenFox.Internal.Constraints;

namespace GoldenFox.Fluent
{
    public abstract class Every : IOperator
    {
        private readonly List<IConstraint> _constraints = new List<IConstraint>();

        private readonly List<Every> _operators = new List<Every>();

        protected Every()
        {
            _operators.Add(this);
        }

        public static WeekBuilder Monday()
        {
            return new WeekBuilder(1);
        }

        public static WeekBuilder Tuesday()
        {
            return new WeekBuilder(2);
        }

        public static WeekBuilder Wednesday()
        {
            return new WeekBuilder(3);
        }

        public static WeekBuilder Thursday()
        {
            return new WeekBuilder(4);
        }

        public static WeekBuilder Friday()
        {
            return new WeekBuilder(5);
        }

        public static WeekBuilder Saturday()
        {
            return new WeekBuilder(6);
        }

        public static WeekBuilder Sunday()
        {
            return new WeekBuilder(7);
        }

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

        public Every And(Every every)
        {
            _operators.Add(every);
            return this;
        }

        public DateTime Evaluate(DateTime @from, bool inclusive = false)
        {
            return new Internal.Operators.First(_operators.Select(x => x.Build()).ToList()).Evaluate(@from, inclusive);
        }

        internal IOperator Build()
        {
            var operatorBuilder = InternalBuild();
            var op = operatorBuilder.Build(_constraints);
            return op;
        }

        internal abstract OperatorBuilder InternalBuild();

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
    }
}