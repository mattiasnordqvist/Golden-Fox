using System;

using GoldenFox.Internal;
using GoldenFox.Internal.Operators.Intervals;

namespace GoldenFox.Fluent
{
    public class WeekBuilder : Every
    {
        private readonly DayOfWeek _day;
        private Timestamp _time = "00:00";

        /// <summary>
        ///
        /// </summary>
        /// <param name="day"> 1 = Monday, 7 = Sunday</param>
        public WeekBuilder(int day)
        {
            _day = (DayOfWeek)(day == 7 ? 0 : day);
        }

        public WeekBuilder At(Timestamp timestamp)
        {
            _time = timestamp;
            return this;
        }

        internal override OperatorBuilder InternalBuild()
        {
            var dayInWeek = new Weekday(_day, _time);
            return new OperatorBuilder(
                x =>
                    {
                        dayInWeek.AddConstraints(x);
                        return dayInWeek;
                    });
        }
    }
}