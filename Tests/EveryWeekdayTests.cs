using System;
using System.Collections.Generic;

using GoldenFox;
using GoldenFox.Internal;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EveryWeekdayTests
    {
        [Test]
        public void EveryMondayAt0630FromJustBefore()
        {
            var expected = new DateTime(2015, 10, 5, 6, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 20, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Monday, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void EveryMondayAt0530StartingOnWednesday()
        {
            var expected = new DateTime(2015, 10, 12, 5, 30, 0);
            var from = new DateTime(2015, 10, 07, 6, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Monday, new Timestamp(5, 30)).Evaluate(from));
        }
        
        [Test]
        public void EveryMondayAt0630FromSameTimeIncludeNow()
        {
            var expected = new DateTime(2015, 10, 5, 6, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Monday, new Timestamp(6, 30)).Evaluate(from, true));
        }

        [Test]
        public void EveryMondayAt0630FromJustAfter()
        {
            var expected = new DateTime(2015, 10, 12, 6, 30, 0);
            var from = new DateTime(2015, 10, 5, 6, 40, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Monday, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void EveryMondayAt0630FromSameTimeDoNotIncludeNow()
        {
            var expected = new DateTime(2015, 10, 12, 6, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Monday, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void EveryMondayAtTwoTimesBothLaterSameDay()
        {
            var expected = new DateTime(2015, 10, 5, 7, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            var sut = new First(new List<IOperator<DateTime>>
                    {
                        new Weekday(DayOfWeek.Monday, new Timestamp(7, 30)),
                        new Weekday(DayOfWeek.Monday, new Timestamp(8, 30)),
                    });
            Assert.AreEqual(expected, sut.Evaluate(from));
        }

        [Test]
        public void EveryMondayAtTwoTimesOneLaterSameDayOneEarlierNextDay()
        {
            var expected = new DateTime(2015, 10, 5, 8, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);

            var sut = new First(new List<IOperator<DateTime>>
            {
                new Weekday(DayOfWeek.Monday, new Timestamp(5, 30)),
                new Weekday(DayOfWeek.Monday, new Timestamp(8, 30)),
            });
            Assert.AreEqual(expected, sut.Evaluate(from));
        }

        [Test]
        public void EverySundayAt0530StartingOnWednesday()
        {
            var expected = new DateTime(2015, 10, 11, 5, 30, 0);
            var from = new DateTime(2015, 10, 07, 6, 30, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Sunday, new Timestamp(5, 30)).Evaluate(from));
        }

        [Test]
        public void EveryThursdayAndSundayAt0530StartingOnWednesday()
        {
            var expected = new DateTime(2015, 10, 8, 5, 30, 0);
            var from = new DateTime(2015, 10, 07, 6, 30, 0);

            var sut = new First(new List<IOperator<DateTime>>
            {
                new Weekday(DayOfWeek.Thursday, new Timestamp(5, 30)),
                new Weekday(DayOfWeek.Sunday, new Timestamp(5, 30)),
            });
            Assert.AreEqual(expected, sut.Evaluate(from));
        }

        [Test]
        public void EveryWednesdayAt0530StartingOnSunday()
        {
            var expected = new DateTime(2015, 10, 14, 5, 30, 0);
            var from = new DateTime(2015, 10, 11, 6, 30, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Wednesday, new Timestamp(5, 30)).Evaluate(from));
        }

        [Test]
        public void EveryTuesdayAt0630FromMonday0730()
        {
            var expected = new DateTime(2015, 10, 6, 6, 30, 0);
            var from = new DateTime(2015, 10, 5, 7, 30, 0);
            Assert.AreEqual(
                expected,
                       new Weekday(DayOfWeek.Tuesday, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void EveryTuesdayAt0730FromMonday0630()
        {
            var expected = new DateTime(2015, 10, 6, 7, 30, 0);
            var from = new DateTime(2015, 10, 5, 6, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Tuesday, new Timestamp(7, 30)).Evaluate(from));
        }

        [Test]
        public void EveryMondayAt0730FromTuesday0630()
        {
            var expected = new DateTime(2015, 10, 12, 7, 30, 0);
            var from = new DateTime(2015, 10, 6, 6, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Monday, new Timestamp(7, 30)).Evaluate(from));
        }

        [Test]
        public void EveryMondayAt0630FromTuesday0730()
        {
            var expected = new DateTime(2015, 10, 12, 6, 30, 0);
            var from = new DateTime(2015, 10, 6, 7, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Monday, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void EveryTuesdayAt0630FromMonday0630IncludeNowShouldStillGiveTuesdayAsFirstDay()
        {
            var expected = new DateTime(2015, 10, 6, 6, 30, 0);
            var from = new DateTime(2015, 10, 5, 6, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Tuesday, new Timestamp(6, 30)).Evaluate(from, true));
        }
    }
}
