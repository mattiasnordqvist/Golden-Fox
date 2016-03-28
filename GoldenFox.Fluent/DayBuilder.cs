using GoldenFox.Internal;
using GoldenFox.Internal.Operators.Intervals;

namespace GoldenFox.Fluent
{
    public class DayBuilder : Every
    {
        private Timestamp _time = "00:00";

        public DayBuilder At(Timestamp timestamp)
        {
            _time = timestamp;
            return this;
        }

        internal override OperatorBuilder InternalBuild()
        {
            var dayInWeek = new Day(_time);
            return new OperatorBuilder(
                x =>
                    {
                        dayInWeek.AddConstraints(x);
                        return dayInWeek;
                    });
        }
    }
}