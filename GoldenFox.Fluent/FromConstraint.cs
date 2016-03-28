using System;

using GoldenFox.Internal.Constraints;

namespace GoldenFox.Fluent
{
    public class FromConstraint
    {
        private readonly DateTime _from;

        public FromConstraint(DateTime from)
        {
            _from = from;
        }

        public From Build()
        {
            return new From(_from);
        }
    }
}