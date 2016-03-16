using System;

namespace GoldenFox.Internal.Constraints
{
    public interface IConstraint
    {
        ConstraintResult Contains(DateTime dateTime);
    }
}