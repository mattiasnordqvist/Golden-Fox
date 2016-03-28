namespace GoldenFox.Fluent
{
    public static class NthExtensions
    {
        public static IntervalBuilderStep1 St(this int n)
        {
            return new IntervalBuilder(n);
        }

        public static IntervalBuilderStep1 Nd(this int n)
        {
            return new IntervalBuilder(n);
        }

        public static IntervalBuilderStep1 Th(this int n)
        {
            return new IntervalBuilder(n);
        }

        public static IntervalBuilderStep1 Rd(this int n)
        {
            return new IntervalBuilder(n);
        }
    }
}