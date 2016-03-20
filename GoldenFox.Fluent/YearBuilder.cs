using GoldenFox.Internal;
using GoldenFox.Internal.Operators.Intervals;

namespace GoldenFox.Fluent
{


    public class YearBuilder : Every
    {
        private readonly int _day;
        private Timestamp _time = "00:00";

        public YearBuilder(int day)
        {
            _day = day;
        }

        public YearBuilder At(Timestamp timestamp)
        {
            _time = timestamp;
            return this;
        }

        internal override OperatorBuilder InternalBuild()
        {
            var dayInYear = new DayInYear(_day, _time);
            return new OperatorBuilder(
                x =>
                    {
                        dayInYear.AddConstraints(x);
                        return dayInYear;
                    });
        }
    }
}