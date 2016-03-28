using GoldenFox.Internal.Operators.Intervals;

namespace GoldenFox.Fluent
{
    public class MinuteBuilder : Every
    {
        private int _offset;

        public MinuteBuilder WithOffset(int seconds)
        {
            _offset = seconds;
            return this;
        }

        internal override OperatorBuilder InternalBuild()
        {
            var minute = new Minute { OffsetInSeconds = _offset };
            return new OperatorBuilder(constraints =>
            {
                minute.AddConstraints(constraints);
                return minute;
            });
        }
    }
}