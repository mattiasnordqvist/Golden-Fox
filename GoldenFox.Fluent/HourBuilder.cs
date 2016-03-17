using GoldenFox.Internal.Operators.Intervals;

namespace GoldenFox.Fluent
{
    public class HourBuilder : Every
    {

        private int _offset;

        public HourBuilder WithOffset(int seconds)
        {
            _offset = seconds;
            return this;
        }


        public HourBuilder WithOffset(int minutes, int seconds)
        {
            _offset = seconds + 60 * minutes;
            return this;
        }

        internal override OperatorBuilder InternalBuild()
        {
            var hour = new Hour { OffsetInSeconds = _offset };
            return new OperatorBuilder(constraints =>
                {
                    hour.AddConstraints(constraints);
                    return hour;
                });
        }
    }
}