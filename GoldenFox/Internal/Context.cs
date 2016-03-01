using System;
using System.Collections.Generic;

using GoldenFox.Internal.Constraints;

namespace GoldenFox.Internal
{
    internal class Context
    {
        public readonly Stack<ExtendedDateTime> DateTimes = new Stack<ExtendedDateTime>();

        public readonly Stack<DateTime> Dates = new Stack<DateTime>();

        public readonly Stack<Timestamp> Timestamps = new Stack<Timestamp>();

        public readonly Stack<IConstraint> Constraints = new Stack<IConstraint>();

        public readonly Stack<int> SecondsOffset = new Stack<int>();
    }
}