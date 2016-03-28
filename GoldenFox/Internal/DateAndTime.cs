using System;
using System.Linq;

namespace GoldenFox.Internal
{
    public class DateAndTime
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int Second { get; set; }
        public int Ms { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }


        public DateAndTime(params int[] components) : this(components.Length > 0 ? components[0] : 0,
            components.Length > 1 ? components[1] : 0,
            components.Length > 2 ? components[2] : 0,
            components.Length > 3 ? components[3] : 0,
            components.Length > 4 ? components[4] : 0,
            components.Length > 5 ? components[5] : 0,
            components.Length > 6 ? components[6] : 0)
        {
        }

        public DateAndTime(int year, int month, int day, int hour = 0, int minute = 0, int second = 0, int ms = 0)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
            Ms = ms;
        }


        public static implicit operator DateAndTime(string value)
        {
            return new DateAndTime(value.Split('-', ' ', ':').Select(int.Parse).ToArray());
        }

        public static implicit operator DateAndTime(DateTime date)
        {
            return new DateAndTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day, Hour, Minute, Second, Ms);
        }
    }
}