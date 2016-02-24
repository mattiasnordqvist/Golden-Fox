using System;

namespace GoldenFox
{
    public interface IOperator<T>
    {
        DateTime Evaluate(DateTime from, bool includeNow = false);
    }
}