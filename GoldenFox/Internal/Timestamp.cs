using System;
using System.Linq;

namespace GoldenFox.Internal
{
    public class Timestamp : IComparable<Timestamp>, IComparable<DateTime>
    {
        public Timestamp(params int[] components) : this(components.Length > 0 ? components[0] : 0,
            components.Length > 1 ? components[1] : 0,
            components.Length > 2 ? components[2] : 0,
            components.Length > 3 ? components[3] : 0)
        {
        }

        public Timestamp(DateTime time) : this(time.Hour, time.Minute, time.Second, time.Millisecond)
        { 
        }

        public Timestamp(int hour = 0, int minute = 0, int second = 0, int ms = 0)
        {
            if (hour > 23 || hour < 0)
            {
                throw new ArgumentException("Hour must be between 0 and 23");
            }

            if (minute < 0 || minute > 59)
            {
                throw new ArgumentException("Minute must be between 0 and 59");
            }

            if (second < 0 || second > 59)
            {
                throw new ArgumentException("Second must be between 0 and 59");
            }

            if (ms < 0 || ms > 999)
            {
                throw new ArgumentException("Ms must be between 0 and 999");
            }

            Hour = hour;
            Minute = minute;
            Second = second;
            Ms = ms;
        }

        public int Second { get; set; }
        public int Ms { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        private int AsComparable => Ms + (Second * 1000) + (Minute * 1000 * 100) + (Hour * 1000 * 100 * 100);

        public int CompareTo(Timestamp other)
        {
            return AsComparable.CompareTo(other.AsComparable);
        }

        public int CompareTo(DateTime other)
        {
            return AsComparable.CompareTo(new Timestamp(other).AsComparable);
        }


        public static implicit operator Timestamp(string value)
        {
            return new Timestamp(value.Split(':').Select(int.Parse).ToArray());
        }

        public static implicit operator Timestamp(DateTime time)
        {
            return new Timestamp(time);
        }
    }
}