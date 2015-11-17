using System;
using System.Linq;

using GoldenFox;

using NUnit.Framework;

namespace Tests.Full
{
    [TestFixture]
    public class GoldenFoxTests
    {
        [Test]
        public void EveryHour()
        {
            var hundredHours = Schedule.Fox("every hour").From(new DateTime(2015, 1, 1, 5, 23, 2)).Take(100).ToList();
            var index = 6;
            Assert.AreEqual(100, hundredHours.Count());
            foreach (var dateTime in hundredHours)
            {
                Assert.AreEqual(index % 24, dateTime.Hour);
                Assert.AreEqual(1 + Math.Floor(index / 24d), dateTime.Day);
                index++;
            }
        }

        [Test]
        public void EveryMinute()
        {
            var totsOfMinutes = Schedule.Fox("every minute").From(new DateTime(2015, 1, 1, 0, 0, 0)).Take(1440).ToList();
            var index = 0;
            Assert.AreEqual(1440, totsOfMinutes.Count());
            foreach (var minute in totsOfMinutes)
            {
                Assert.AreEqual(index % 60, minute.Minute);
                Assert.AreEqual(Math.Floor(index / 60d), minute.Hour);
                index++;
            }
        }

        [Test]
        public void TenDaysFromNowEveryDayAtTenOClock()
        {
            var now = DateTime.Now;
            var tenDaysFromNow = Schedule.Fox("every day @ 10:00").From(now).Take(10);
            var index = now.Hour >= 10 ? 1 : 0;
            foreach (var dateTime in tenDaysFromNow)
            {
                Assert.AreEqual(now.AddDays(index).Day, dateTime.Day);
                index++;
            }
        }

        [Test]
        public void LastDayEveryMonthForAYear()
        {
            var now = DateTime.Parse("1984-01-01 12:00");
            var _12months = Schedule.Fox("last day every month @ 12:00").From(now).Take(12);
            int i = 1;
            foreach (var dateTime in _12months)
            {
                Assert.AreEqual(i, dateTime.Month);
                Assert.AreEqual(DateTime.DaysInMonth(1984, i), dateTime.Day);
                Assert.AreEqual(1984, dateTime.Year);
                i++;
            }
        }

        [Test]
        public void _2ndLastDayEveryMonthForAYear()
        {
            var now = DateTime.Parse("1984-01-01 12:00");
            var _12months = Schedule.Fox("2nd last day every month @ 12:00").From(now).Take(12);
            int i = 1;
            foreach (var dateTime in _12months)
            {
                Assert.AreEqual(i, dateTime.Month);
                Assert.AreEqual(DateTime.DaysInMonth(1984, i) - 1, dateTime.Day);
                Assert.AreEqual(1984, dateTime.Year);
                i++;
            }
        }

        [Test]
        public void _2ndLastDayEveryMonthForAYear_FirstMonthJustPassed()
        {
            var now = DateTime.Parse("1984-01-30 16:00");
            var _11months = Schedule.Fox("2nd last day every month @ 12:00").From(now).Take(11);
            int i = 2;
            foreach (var dateTime in _11months)
            {
                Assert.AreEqual(i, dateTime.Month);
                Assert.AreEqual(DateTime.DaysInMonth(1984, i) - 1, dateTime.Day);
                Assert.AreEqual(1984, dateTime.Year);
                i++;
            }
        }

        [Test]
        public void _3rdLastDayEveryMonthForAYear()
        {
            var now = DateTime.Parse("1984-01-01 00:00");
            var _12months = Schedule.Fox("3rd last day every month @ 12:00").From(now).Take(12);
            int i = 1;
            foreach (var dateTime in _12months)
            {
                Assert.AreEqual(i, dateTime.Month);
                Assert.AreEqual(DateTime.DaysInMonth(1984, i) - 2, dateTime.Day);
                Assert.AreEqual(1984, dateTime.Year);
                i++;
            }
        }

        [Test]
        public void _3rdLastDayEveryMonthForAYear_FirstMonthJustPassed()
        {
            var now = DateTime.Parse("1984-01-29 13:00");
            var _11months = Schedule.Fox("3rd last day every month @ 12:00").From(now).Take(11);
            int i = 2;
            foreach (var dateTime in _11months)
            {
                Assert.AreEqual(i, dateTime.Month);
                Assert.AreEqual(DateTime.DaysInMonth(1984, i) - 2, dateTime.Day);
                Assert.AreEqual(1984, dateTime.Year);
                i++;
            }
        }

        [Test]
        public void Yadayadayada()
        {
            var now = DateTime.Parse("1984-01-01 00:00");
            var _10months = Schedule.Fox("22nd day every month and 15th day every month @ 12:00").From(now).Take(10);
            bool fifteenth = true;
            int i = 1;
            foreach (var dateTime in _10months)
            {
                Assert.AreEqual(fifteenth ? 15 : 22, dateTime.Day);
                Assert.AreEqual((int)Math.Ceiling(i / 2d), dateTime.Month);
                Assert.AreEqual(1984, dateTime.Year);
                fifteenth = !fifteenth;
                i++;
            }
        }
    }
}