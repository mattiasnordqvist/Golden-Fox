namespace GoldenFox.Fluent
{
    public class IntervalBuilder : IntervalBuilderStep1, IntervalBuilderStep2
    {
        private int _n;

        public IntervalBuilder(int n)
        {
            _n = n;
        }

        IntervalBuilderStep1 IntervalBuilderStep1.Last()
        {
            _n = -_n;
            return this;
        }

        IntervalBuilderStep2 IntervalBuilderStep1.DayEvery()
        {
            return this;
        }

        YearBuilder IntervalBuilderStep2.Year()
        {
            return new YearBuilder(_n);
        }
    }
}