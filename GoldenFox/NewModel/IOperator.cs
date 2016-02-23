using System;

namespace GoldenFox.NewModel
{
    public interface IOperator<T>
    {
        DateTime Evaluate(DateTime from, bool includeNow = false);
    }
}