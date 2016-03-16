using System;

using GoldenFox.Internal.Constraints;

namespace GoldenFox.Fluent
{
    public class UntilConstraint
    {
        private readonly DateTime _until;

        public UntilConstraint(DateTime until)
        {
            _until = until;
        }
        public IConstraint Build()
        {
            return new Until(_until);
        }
    }
}