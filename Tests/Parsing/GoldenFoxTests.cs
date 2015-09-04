using System;
using System.Linq;

using GoldenFox.Parsing;

using NUnit.Framework;

namespace Tests.Parsing
{
    [TestFixture]
    public class GoldenFoxTests
    {
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
    }
}