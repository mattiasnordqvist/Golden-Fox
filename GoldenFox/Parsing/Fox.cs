using System;
using System.Collections;
using System.Collections.Generic;

namespace GoldenFox.Parsing
{
    public class Fox : IEnumerable<DateTime>
    {
        private readonly Schedule _schedule;
        private DateTime _current;

        public Fox(string schedule, DateTime startFrom)
        {
            _schedule = new Schedule(schedule);
            _current = startFrom;
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            while (true)
            {
                _current = _schedule.Next(_current).AddMinutes(1);
                yield return _current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}