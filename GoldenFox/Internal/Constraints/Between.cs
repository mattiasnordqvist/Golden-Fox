using System;

namespace GoldenFox.Internal.Constraints
{
    public class Between : IConstraint
    {
        public Between(Timestamp from, Timestamp to)
        {
            From = @from;
            To = to;
        }

        public Timestamp From { get; set; }

        public Timestamp To { get; set; }

        public ConstraintResult Contains(DateTime dateTime)
        {
            var toEarly = From.CompareTo(new Timestamp(dateTime)) > 0;
            var toLate = To.CompareTo(new Timestamp(dateTime)) < 0;
            var passed = !(toEarly || toLate);
            return new ConstraintResult
            {
                Passed = passed,
                ClosestValidFutureInput = 
                    passed 
                        ? dateTime 
                        : toEarly 
                            ? dateTime.SetTime(From)
                            : dateTime.AddDays(1).SetTime(From),
            };
        }
    }
}