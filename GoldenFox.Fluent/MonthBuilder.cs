using GoldenFox.Internal;
using GoldenFox.Internal.Operators.Intervals;

namespace GoldenFox.Fluent
{
    public class MonthBuilder : Every
    {
        private readonly int _day;
        private Timestamp _time = "00:00";

        public MonthBuilder(int day)
        {
            _day = day;
        }

        public MonthBuilder At(Timestamp timestamp)
        {
            _time = timestamp;
            return this;
        }

        internal override OperatorBuilder InternalBuild()
        {
            var dayInMonth = new DayInMonth(_day, _time);
            return new OperatorBuilder(
                x =>
                    {
                        dayInMonth.AddConstraints(x);
                        return dayInMonth;
                    });
        }
    }
}