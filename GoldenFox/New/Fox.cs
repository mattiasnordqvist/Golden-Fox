using System;
using System.Collections;
using System.Collections.Generic;

namespace GoldenFox.New
{
    public class Fox : IEnumerable<DateTime>
    {
        private readonly Schedule _schedule;
        private DateTime _current;

        public Fox(string schedule, DateTime startFrom)
        {
            _schedule = new ScheduleParser().Parse(schedule);
            _current = startFrom;
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            var includeNow = true;
            while (true)
            {
                _current = _schedule.Next(_current, includeNow);
                yield return _current;
                includeNow = false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}