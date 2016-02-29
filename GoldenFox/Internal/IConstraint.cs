using System;

namespace GoldenFox.Internal
{
    internal interface IConstraint
    {
        ConstraintResult Contains(DateTime dateTime, bool includeNow);
    }
}