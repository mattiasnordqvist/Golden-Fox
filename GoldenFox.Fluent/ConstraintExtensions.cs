using System;

using GoldenFox.Internal;

namespace GoldenFox.Fluent
{
    public static class ConstraintExtensions
    {
        public static BetweenConstraint Between(this Every @this, Timestamp from)
        {
            return new BetweenConstraint(@this, from);
        }

        public static Every From(this Every @this, DateTime from)
        {
            var c = new FromConstraint(from);
            @this.AddConstraint(c.Build());
            return @this;
        }

        public static Every Until(this Every @this, DateTime until)
        {
            var c = new UntilConstraint(until);
            @this.AddConstraint(c.Build());
            return @this;
        }
    }
}