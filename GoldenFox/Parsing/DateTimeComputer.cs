using System;
using System.Collections.Generic;

namespace GoldenFox.Parsing
{
    public class DateTimeComputer
    {
        private readonly PartsTraverser _partsTraverser;

        public DateTimeComputer(List<string> parts)
        {
            _partsTraverser = new PartsTraverser(parts);
        }

        public DateTime Next(DateTime given)
        {
            var next = given;

            var firstPart = _partsTraverser.NextPart();
            var daysOffset = 0;
            var pendingTimeDependingOffset = 0;
            if (firstPart == "every")
            {
                var weekdayPart = _partsTraverser.NextPart();
                if (weekdayPart == "day")
                {
                    daysOffset = 0;
                    pendingTimeDependingOffset = 1;
                }
                else
                {
                    daysOffset = next.DaysTo(weekdayPart);
                    pendingTimeDependingOffset = 7;
                }
            }
            else if (DateTimeExtensions.IsWeekday(firstPart))
            {
                _partsTraverser.Skip("s");
                daysOffset = next.DaysTo(firstPart);
                pendingTimeDependingOffset = 7;
            }
            else if (DateTimeExtensions.IsNthWeekday(firstPart))
            {
                var firstPartAsInt = DateTimeExtensions.WeekdayAsInt(firstPart);
                var last = _partsTraverser.SkipIf("last");
                _partsTraverser.Skip("day").Skip("every");
                if (_partsTraverser.NextIf("week"))
                {
                    pendingTimeDependingOffset = 7;
                    if (last)
                    {
                        firstPartAsInt = 7 - firstPartAsInt + 1;
                    }

                    daysOffset = next.DaysTo(firstPartAsInt);
                }
                else if (_partsTraverser.NextIf("month"))
                {
                    var daysInMonth = DateTime.DaysInMonth(next.Year, next.Month);
                    var offsetInMonth = daysInMonth - firstPartAsInt + 1;
                    var totalOffsetIfThisMonth = offsetInMonth - next.Day;
                    var daysInNextMonth = DateTime.DaysInMonth(next.AddMonths(1).Year, next.AddMonths(1).Month);
                    var offsetInNextMonth = daysInNextMonth - firstPartAsInt + 1;
                    var totalOffsetIfNextMonth = offsetInNextMonth + daysInMonth - next.Day;
                    if (offsetInMonth > next.Day)
                    {
                        daysOffset = totalOffsetIfThisMonth;
                    }
                    else
                    {
                        daysOffset = totalOffsetIfNextMonth;
                    }
                }
            }
            else if (firstPart == "last")
            {
                _partsTraverser.Skip("day").Skip("every");

                if (_partsTraverser.NextIf("week"))
                {
                    pendingTimeDependingOffset = 7;
                    daysOffset = next.DaysTo(7);
                }
                else if (_partsTraverser.NextIf("month"))
                {
                    var daysInMonth = DateTime.DaysInMonth(next.Year, next.Month);
                    var daysInNextMonth = DateTime.DaysInMonth(next.AddMonths(1).Year, next.AddMonths(1).Month);
                    pendingTimeDependingOffset = daysInMonth == next.Day ? daysInNextMonth : next.DaysToDate(daysInNextMonth);
                    daysOffset = next.DaysToDate(daysInMonth);
                }
            }

            next = next.AddDays(daysOffset);

            var timePart = _partsTraverser.NextPart();
            Time time;
            if (timePart != null && (timePart == "at" || timePart == "@"))
            {
                time = new TimeParser(_partsTraverser).Parse();
            }
            else
            {
                time = new Time { Hour = 0, Minute = 0 };
            }

            if (time.IsLess(new Time { Hour = next.Hour, Minute = next.Minute }))
            {
                next = next.AddDays(pendingTimeDependingOffset);
            }

            time.ApplyTo(ref next);

            next = next.StripSecondsAndBelow();
            return next;
        }
    }
}