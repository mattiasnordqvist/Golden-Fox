using System;

namespace GoldenFox
{
    public class Clock : IComparable<Clock>
    {
        public Clock() { }

        public Clock(int hh, int mm)
        {
            Hour = hh;
            Minute = mm;
            Seconds = 00;
        }

        public Clock(int hh)
        {
            Hour = hh;
            Minute = 00;
            Seconds = 00;
        }

        public Clock(int hh, int mm, int ss)
        {
            Hour = hh;
            Minute = mm;
            Seconds = ss;
        }

        public Clock(DateTime d)
        {
            Hour = d.Hour;
            Minute = d.Minute;
            Seconds = d.Second;
        }

        public Clock(string s)
        {
            Hour = int.Parse(s.Split(':')[0]);
            Minute = int.Parse(s.Split(':')[1]);
            Seconds = int.Parse(s.Split(':')[2]);
        }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Seconds { get; set; }
        
        public int CompareTo(Clock other)
        {
            return other.Hour == Hour
                       ? (other.Minute == Minute
                              ? (other.Seconds == Seconds ? 0 : Seconds - other.Seconds)
                              : Minute - other.Minute)
                       : Hour - other.Hour;
        }
    }
}