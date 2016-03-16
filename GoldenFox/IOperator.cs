using System;
using System.Collections.Generic;

using GoldenFox.Internal.Constraints;

namespace GoldenFox
{
    public interface IOperator
    {
        DateTime Evaluate(DateTime from, bool inclusive = false);
    }
}