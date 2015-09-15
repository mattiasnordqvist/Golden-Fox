using System;
using System.Collections.Generic;

using GoldenFox.New;

using NUnit.Framework;

namespace Tests.New
{
    [TestFixture]
    public class EveryMonthsTests
    {
        [Test]
        public void FirstDayEveryMonthAt0630()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 01, 06, 30, 00),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 0, Interval.Month).Next(
                    new DateTime(2015, 09, 05, 12, 00, 00),
                    true));
        }

        [Test]
        public void FirstDayEveryMonthAt0630SkipToNextYear()
        {
            Assert.AreEqual(
                new DateTime(2016, 01, 01, 06, 30, 00),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 0, Interval.Month).Next(
                    new DateTime(2015, 12, 19, 12, 00, 00),
                    true));
        }

        [Test]
        public void SecondDayEveryMonthAt0630()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 02, 06, 30, 00),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 1, Interval.Month).Next(
                    new DateTime(2015, 09, 05, 12, 00, 00),
                    true));
        }

        [Test]
        public void SecondDayEveryMonthAt0630JumpJustOneDay()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 02, 06, 30, 00),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 1, Interval.Month).Next(
                    new DateTime(2015, 10, 01, 12, 00, 00),
                    true));
        }

        [Test]
        public void LastDayEveryMonthAt0630()
        {
            Assert.AreEqual(
                new DateTime(2015, 09, 30, 06, 30, 00),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, -1, Interval.Month).Next(
                    new DateTime(2015, 09, 05, 12, 00, 00),
                    true));
        }

        [Test]
        public void SecondLastDayEveryMonthAt0630()
        {
            Assert.AreEqual(
                new DateTime(2015, 09, 29, 06, 30, 00),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, -2, Interval.Month).Next(
                    new DateTime(2015, 09, 05, 12, 00, 00),
                    true));
        }

        [Test]
        public void SecondLastDayEveryMonthAt0630TestFebruary()
        {
            Assert.AreEqual(
                new DateTime(2015, 02, 27, 06, 30, 00),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, -2, Interval.Month).Next(
                    new DateTime(2015, 02, 05, 12, 00, 00),
                    true));
        }
    }
}