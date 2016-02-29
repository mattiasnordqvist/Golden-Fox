using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.Internal
{
    internal abstract class Interval : IOperator<DateTime>
    {
        protected readonly List<IConstraint> Constraints = new List<IConstraint>();

        public virtual DateTime Evaluate(DateTime dateTime, bool inclusive = false)
        {
            var candidate = ApplyLowerBoundConstraints(dateTime);
            if (candidate != dateTime)
            {
                return Evaluate(candidate, true);
            }

            candidate = ApplyRule(candidate, inclusive);

            var afterUpperboundApplied = ApplyUpperBoundConstraints(candidate);
            if (afterUpperboundApplied != candidate)
            {
                return Evaluate(afterUpperboundApplied, true);
            }
            return candidate;
        }

        protected abstract DateTime ApplyRule(DateTime candidate, bool inclusive);

        public void AddConstraint(IConstraint constraint)
        {
            Constraints.Add(constraint);
        }

        public void AddConstraints(List<IConstraint> constraints)
        {
            Constraints.AddRange(constraints);
        }

        public DateTime ApplyUpperBoundConstraints(DateTime datetime)
        {
            var candidate = datetime;
            Until()?.Contains(candidate);

            
            if (Between() != null)
            {
                var result = Between().Contains(candidate);
                if (!result.Passed)
                {
                    candidate = result.ClosestValidFutureInput;
                }
            }
            return candidate;
        }

        private From From()
        {
            if (Constraints.Any(x => x.GetType() == typeof(From)))
            {
                return (From)Constraints.First(x => x.GetType() == typeof(From));
            }
            return null;
        }

        private Until Until()
        {
            if (Constraints.Any(x => x.GetType() == typeof(Until)))
            {
                return (Until)Constraints.First(x => x.GetType() == typeof(Until));
            }
            return null;
        }

        private Between Between()
        {
            if (Constraints.Any(x => x.GetType() == typeof(Between)))
            {
                return (Between)Constraints.First(x => x.GetType() == typeof(Between));
            }
            return null;
        }

        protected DateTime ApplyLowerBoundConstraints(DateTime dateTime)
        {
            var candidate = dateTime;
            if (From() != null)
            {
                var result = From().Contains(candidate);
                if (!result.Passed)
                {
                    candidate = result.ClosestValidFutureInput;
                } 
            }

            if (Between() != null)
            {
                var result = Between().Contains(candidate);
                if (!result.Passed)
                {
                    candidate = result.ClosestValidFutureInput;
                }
            }

            return candidate;

        }
    }
}