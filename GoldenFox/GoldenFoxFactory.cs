using System;

namespace GoldenFox
{
    public class GoldenFoxFactory
    {
        private readonly string _schedule;

        internal GoldenFoxFactory(string schedule)
        {
            _schedule = schedule;
        }

        public Fox From(DateTime from)
        {
            return new Fox(_schedule, from);
        }
    }
}