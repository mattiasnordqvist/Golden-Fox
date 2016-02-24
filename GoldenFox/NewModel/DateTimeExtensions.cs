using System;

namespace GoldenFox.NewModel
{
    public static class DateTimeExtensions
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

        public static DateTime SetDay(this DateTime @this, int day)
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
    }
}