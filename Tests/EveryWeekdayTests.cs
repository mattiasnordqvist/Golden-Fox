using System;
using System.Collections.Generic;

using GoldenFox;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EveryWeekdayTests
    {
        [Test]
        public void EveryMondayAt0630FromSameTimeIncludeNow()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 5, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 0, Interval.Week)
                    .Next(new DateTime(2015, 10, 05, 6, 30, 0), true));
        }

        [Test]
        public void EveryMondayAt0630FromJustBefore()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 5, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 0, Interval.Week)
                .Next(new DateTime(2015, 10, 05, 6, 20, 0)));
        }

        [Test]
        public void EveryMondayAt0630FromJustAfter()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 12, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 0, Interval.Week)
                .Next(new DateTime(2015, 10, 05, 6, 40, 0)));
        }

        [Test]
        public void EveryMondayAt0630FromSameTimeDoNotIncludeNow()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 12, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 0, Interval.Week)
                    .Next(new DateTime(2015, 10, 05, 6, 30, 0)));
        }

        [Test]
        public void EveryMondayAtTwoTimesBothLaterSameDay()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 5, 7, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 7, Minute = 30 }, new Clock { Hour = 8, Minute = 30 } }, 0, Interval.Week)
                .Next(new DateTime(2015, 10, 05, 6, 30, 0)));
        }

        [Test]
        public void EveryMondayAtTwoTimesOneLaterSameDayOneEarlierNextDay()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 5, 8, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 5, Minute = 30 }, new Clock { Hour = 8, Minute = 30 } }, 0, Interval.Week)
                .Next(new DateTime(2015, 10, 05, 6, 30, 0)));
        }

        [Test]
        public void EveryMondayAt0530StartingOnWednesday()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 12, 5, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 5, Minute = 30 } }, 0, Interval.Week)
                .Next(new DateTime(2015, 10, 07, 6, 30, 0)));
        }

        [Test]
        public void EverySundayAt0530StartingOnWednesday()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 11, 5, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 5, Minute = 30 } }, 6, Interval.Week)
                .Next(new DateTime(2015, 10, 07, 6, 30, 0)));
        }

        [Test]
        public void EveryThursdayAndSundayAt0530StartingOnWednesday()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 8, 5, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 5, Minute = 30 } }, new List<int> { 3, 6 }, Interval.Week)
                .Next(new DateTime(2015, 10, 07, 6, 30, 0)));
        }

        [Test]
        public void EveryWednesdayAt0530StartingOnSunday()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 14, 5, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 5, Minute = 30 } }, 2, Interval.Week)
                .Next(new DateTime(2015, 10, 11, 6, 30, 0)));
        }

        [Test]
        public void EveryTuesdayAt0630FromMonday0730()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 6, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 1, Interval.Week)
                .Next(new DateTime(2015, 10, 5, 7, 30, 0)));
        }

        [Test]
        public void EveryTuesdayAt0730FromMonday0630()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 6, 7, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 7, Minute = 30 } }, 1, Interval.Week)
                .Next(new DateTime(2015, 10, 5, 6, 30, 0)));
        }

        [Test]
        public void EveryMondayAt0730FromTuesday0630()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 12, 7, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 7, Minute = 30 } }, 0, Interval.Week)
                .Next(new DateTime(2015, 10, 6, 6, 30, 0)));
        }

        [Test]
        public void EveryMondayAt0630FromTuesday0730()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 12, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 0, Interval.Week)
                .Next(new DateTime(2015, 10, 6, 7, 30, 0)));
        }

        [Test]
        public void EveryTuesdayAt0630FromMonday0630IncludeNowShouldStillGiveTuesdayAsFirstDay()
        {
            Assert.AreEqual(
                new DateTime(2015, 10, 6, 6, 30, 0),
                new Schedule(new List<Clock> { new Clock { Hour = 6, Minute = 30 } }, 1, Interval.Week)
                .Next(new DateTime(2015, 10, 5, 6, 30, 0), true));
        }
    }
}
