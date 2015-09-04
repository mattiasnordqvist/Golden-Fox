using System;

namespace GoldenFox.Parsing
{
    public static class DateTimeExtensions
    {
        private static readonly string[] Weekdays = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
        private static readonly string[] NthWeekdays = { "1st", "2nd", "3rd", "4th", "5th", "6th", "7th" };

        public static DateTime SkipToNextWeek(this DateTime @this)
        {
            return @this.AddDays(7 - (int)@this.DayOfWeek + 1).Date;
        }

        public static DateTime SkipToNextMonth(this DateTime @this)
        {
            return @this.AddMonths(1).AddDays(-@this.Day + 1).Date;
        }

        public static int DaysTo(this DateTime @this, string weekday)
        {
            return DaysTo(@this, WeekdayAsInt(weekday));
        }

        public static int DaysTo(this DateTime @this, int toWeekday)
        {
            var thisWeekDay = (int)@this.DayOfWeek;

            if (toWeekday >= thisWeekDay)
            {
                return toWeekday - thisWeekDay;
            }
            else
            {
                return 7 + toWeekday - thisWeekDay;
            }
        }

        public static int DaysToDate(this DateTime @this, int toDate)
        {
            var thisDay = @this.Day;

            if (toDate >= thisDay)
            {
                return toDate - thisDay;
            }
            else
            {
                return DateTime.DaysInMonth(@this.Year, @this.Month) - @this.Day + toDate;
            }
        }

        public static int WeekdayAsInt(string weekday)
        {
            if (IsWeekday(weekday))
            {
                return Array.IndexOf(Weekdays, weekday) + 1;
            }
            else if (IsNthWeekday(weekday))
            {
                return Array.IndexOf(NthWeekdays, weekday) + 1;
            }
            else
            {
                return -1;
            }
        }

        public static bool IsWeekday(string weekday)
        {
            return Array.IndexOf(Weekdays, weekday) >= 0;
        }

        public static bool IsNthWeekday(string nthWeekday)
        {
            return Array.IndexOf(NthWeekdays, nthWeekday) >= 0;
        }

        public static DateTime StripSecondsAndBelow(this DateTime next)
        {
            next = next.AddSeconds(-next.Second);
            next = next.AddMilliseconds(-next.Millisecond);
            return next;
        }
    }
}