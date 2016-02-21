using System;

namespace GoldenFox.NewModel
{
    public interface IOperator<T>
    {
        DateTime Evaluate();
    }
}