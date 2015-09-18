using System;
using System.Collections.Generic;

using GoldenFox;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EveryDayTests
    {
        [Test]
        public void EveryDayAt0630FromSameTimeIncludeNow()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 5, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, new List<int> { 0, 1, 2, 3, 4, 5, 6 }, Interval.Week)
                    .Next(new DateTime(2015, 10, 05, 6, 30, 0), true));
        }

        [Test]
        public void EveryDayAt0630FromJustBefore()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 5, 6, 30, 0),
                
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, new List<int> { 0, 1, 2, 3, 4, 5, 6 }, Interval.Week)
                .Next(new DateTime(2015, 10, 05, 6, 20, 0)));
        }

        [Test]
        public void EveryDayAt0630FromJustAfter()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 6, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, new List<int> { 0, 1, 2, 3, 4, 5, 6 }, Interval.Week)
                .Next(new DateTime(2015, 10, 05, 6, 40, 0)));
        }

        [Test]
        public void EveryDayAt0630FromSameTimeDoNotIncludeNow()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 6, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, new List<int> { 0, 1, 2, 3, 4, 5, 6 }, Interval.Week)
                    .Next(new DateTime(2015, 10, 05, 6, 30, 0)));
        }

        [Test]
        public void EveryDayAtTwoTimesBothLaterSameDay()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 5, 7, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 7, Minute = 30 }, new Clock { Hour = 8, Minute = 30 } }, new List<int> { 0, 1, 2, 3, 4, 5, 6 }, Interval.Week)
                .Next(new DateTime(2015, 10, 05, 6, 30, 0)));
        }

        [Test]
        public void EveryDayAtTwoTimesOneLaterSameDayOneEarlierNextDay()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 5, 8, 30, 0),
                new Schedule(
                    new List<Clock> { new Clock { Hour = 5, Minute = 30 }, new Clock { Hour = 8, Minute = 30 } }, new List<int> { 0, 1, 2, 3, 4, 5, 6 }, Interval.Week)
                .Next(new DateTime(2015, 10, 05, 6, 30, 0)));
        }
    }
}
