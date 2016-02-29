using System;

namespace GoldenFox.Internal
{
    internal static class DateTimeExtensions
    {
        public static DateTime SetTime(this DateTime @this, Timestamp timestamp)
        {
            return
                @this.Date.AddHours(timestamp.Hour)
                    .AddMinutes(timestamp.Minute)
                    .AddSeconds(timestamp.Second)
                    .AddMilliseconds(timestamp.Ms);
        }

        public static int DaysOfMonth(this DateTime @this)
        {
            return DateTime.DaysInMonth(@this.Year, @this.Month);
        }

        public static int DaysOfYear(this DateTime @this)
        {
            return DateTime.IsLeapYear(@this.Year) ? 366 : 365;
        }


        public static DateTime SetDayInMonth(this DateTime @this, int day)
        {
            if (day > 0)
            {
                return @this.AddDays(-@this.Day).AddDays(day);
            }
            else
            {
                return @this.AddMonths(1).AddDays(-@this.AddMonths(1).Day).AddDays(day);
            }
        }

        public static DateTime SetDayInYear(this DateTime @this, int day)
        {
            if (day > 0)
            {
                return @this.AddDays(-@this.DayOfYear).AddDays(day);
            }
            else
            {
                return @this.AddYears(1).AddDays(-@this.AddYears(1).DayOfYear).AddDays(day);
            }
        }

        /// <summary>
        /// Return the date with minutes, seconds and milliseconds stripped
        /// </summary>
        public static DateTime StripMinutes(this DateTime @this)
        {
            return @this.AddMinutes(-@this.Minute).StripSeconds();
        }

        /// <summary>
        /// Return the date with seconds and milliseconds stripped
        /// </summary>
        public static DateTime StripSeconds(this DateTime @this)
        {
            return @this.AddSeconds(-@this.Second).StripMilliseconds();
        }

        /// <summary>
        /// Return the date with milliseconds stripped
        /// </summary>
        public static DateTime StripMilliseconds(this DateTime @this)
        {
            return @this.AddMilliseconds(-@this.Millisecond);
        }
    }
}