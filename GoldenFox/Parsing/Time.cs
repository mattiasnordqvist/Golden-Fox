using System;

namespace GoldenFox.Parsing
{
    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        public void ApplyTo(ref DateTime t)
        {
            t = t.AddHours(Hour - t.Hour);
            t = t.AddMinutes(Minute - t.Minute);
        }

        public bool IsLess(Time time)
        {
            if (Hour < time.Hour)
            {
                return true;
            }

            if (Hour == time.Hour && Minute < time.Minute)
            {
                return true;
            }

            return false;
        }

        public bool IsLessOrEqual(Time time)
        {
            if (Hour <= time.Hour)
            {
                return true;
            }

            if (Hour == time.Hour && Minute <= time.Minute)
            {
                return true;
            }

            return false;
        }
    }
}