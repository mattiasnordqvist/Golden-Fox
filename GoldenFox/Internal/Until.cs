using System;
using System.Globalization;

namespace GoldenFox.Internal
{
    internal class Until : IConstraint
    {
        private readonly DateTime _until;
        public Until(DateTime until)
        {
            _until = until;
        }

        public ConstraintResult Contains(DateTime dateTime, bool includeNow)
        {
            if (includeNow ? _until >= dateTime : _until > dateTime)
            {
                return new ConstraintResult { Passed = true, ClosestValidFutureInput = dateTime, };
            }
            else
            {
                throw new InvalidOperationException(
                    $"The given point in time, {dateTime.ToString(CultureInfo.InvariantCulture)}, occurs after {_until.ToString(CultureInfo.InvariantCulture)} which has been used in an 'until'-constraint. Therefore, next occurence can not possibly be calculated.");
            }
        }
    }
}