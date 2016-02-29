using System;

namespace GoldenFox.Internal.Constraints
{
    internal interface IConstraint
    {
        ConstraintResult Contains(DateTime dateTime);
    }
}