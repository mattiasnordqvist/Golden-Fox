using System;

namespace GoldenFox
{
    public interface IOperator
    {
        DateTime Evaluate(DateTime from, bool inclusive = false);
    }
}