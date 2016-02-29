using System;

namespace GoldenFox.Internal
{
    internal class ExtendedDateTime
    {
        public ExtendedDateTime()
        {
            TimeSpecified = true;
        }

        public DateTime DateTime { get; set; }

        public bool TimeSpecified { get; private set; }

        public void TimeNotSpecified()
        {
            TimeSpecified = false;
        }
    }
}