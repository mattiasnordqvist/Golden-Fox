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
    }
}