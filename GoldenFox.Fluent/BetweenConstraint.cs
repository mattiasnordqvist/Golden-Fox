using GoldenFox.Internal;
using GoldenFox.Internal.Constraints;
using GoldenFox.Internal.Operators.Intervals;

namespace GoldenFox.Fluent
{
    public class BetweenConstraint
    {
        private readonly Every _b;

        private readonly Timestamp _from;
        private Timestamp _to;

        public BetweenConstraint(Every b, Timestamp time)
        {
            _b = b;
            _from = time;
        }

        public Every And(Timestamp to)
        {
            _to = to;
            
            _b.AddConstraint(new Between(_from, _to));
            return _b;
        }
    }
}