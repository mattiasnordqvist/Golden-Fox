using System;
using System.Collections.Generic;

using GoldenFox.NewModel;

using NUnit.Framework;

namespace Tests.NewModel
{
    [TestFixture]
    public class EveryWeekdayTests
    {
        [Test]
        public void EveryMondayAt0630FromJustBefore()
        {
            var expected = new DateTime(2015, 10, 5, 6, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 20, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Monday, new Timestamp(6, 30), new From(from)).Evaluate()
            );
        }

        [Test]
        public void EveryMondayAt0530StartingOnWednesday()
        {
            var expected = new DateTime(2015, 10, 12, 5, 30, 0);
            var from = new DateTime(2015, 10, 07, 6, 30, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Monday, new Timestamp(5, 30), new From(from)).Evaluate()
            );
        }
        
        [Test]
        public void EveryMondayAt0630FromSameTimeIncludeNow()
        {
            var expected = new DateTime(2015,10,5,6,30,0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Monday, new Timestamp(6, 30), new From(from), true).Evaluate()
            );
        }



        [Test]
        public void EveryMondayAt0630FromJustAfter()
        {
            var expected = new DateTime(2015, 10, 12, 6, 30, 0);
            var from = new DateTime(2015, 10, 5, 6, 40, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Monday, new Timestamp(6, 30), new From(from)).Evaluate());
        }

        [Test]
        public void EveryMondayAt0630FromSameTimeDoNotIncludeNow()
        {
            var expected = new DateTime(2015, 10, 12, 6, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Monday, new Timestamp(6, 30), new From(from)).Evaluate());
        }

        [Test]
        public void EveryMondayAtTwoTimesBothLaterSameDay()
        {
            var expected = new DateTime(2015, 10, 5, 7, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);
            var sut = new First(new List<IOperator<DateTime>>
                    {
                        new Weekday(DayOfWeek.Monday, new Timestamp(7, 30), new From(@from)),
                        new Weekday(DayOfWeek.Monday, new Timestamp(8, 30), new From(@from)),
                    });
            Assert.AreEqual(expected, sut.Evaluate());
        }

        [Test]
        public void EveryMondayAtTwoTimesOneLaterSameDayOneEarlierNextDay()
        {
            var expected = new DateTime(2015, 10, 5, 8, 30, 0);
            var from = new DateTime(2015, 10, 05, 6, 30, 0);

            var sut = new First(new List<IOperator<DateTime>>
            {
                new Weekday(DayOfWeek.Monday, new Timestamp(5, 30), new From(@from)),
                new Weekday(DayOfWeek.Monday, new Timestamp(8, 30), new From(@from)),
            });
            Assert.AreEqual(expected, sut.Evaluate());
        }

        [Test]
        public void EverySundayAt0530StartingOnWednesday()
        {
            var expected = new DateTime(2015, 10, 11, 5, 30, 0);
            var from = new DateTime(2015, 10, 07, 6, 30, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Sunday, new Timestamp(5, 30), new From(from)).Evaluate());
        }

        [Test]
        public void EveryThursdayAndSundayAt0530StartingOnWednesday()
        {
            var expected = new DateTime(2015, 10, 8, 5, 30, 0);
            var from = new DateTime(2015, 10, 07, 6, 30, 0);

            var sut = new First(new List<IOperator<DateTime>>
            {
                new Weekday(DayOfWeek.Thursday, new Timestamp(5, 30), new From(@from)),
                new Weekday(DayOfWeek.Sunday, new Timestamp(5, 30), new From(@from)),
            });
            Assert.AreEqual(expected, sut.Evaluate());
        }

        [Test]
        public void EveryWednesdayAt0530StartingOnSunday()
        {
            var expected = new DateTime(2015, 10, 14, 5, 30, 0);
            var from = new DateTime(2015, 10, 11, 6, 30, 0);
            Assert.AreEqual(
                expected,
                new Weekday(DayOfWeek.Wednesday, new Timestamp(5, 30), new From(from)).Evaluate());
        }

        [Test]
        public void EveryTuesdayAt0630FromMonday0730()
        {
            var expected = new DateTime(2015, 10, 6, 6, 30, 0);
            var from = new DateTime(2015, 10, 5, 7, 30, 0);
            Assert.AreEqual(
                expected,
                       new Weekday(DayOfWeek.Tuesday, new Timestamp(6, 30), new From(from)).Evaluate());
        }

        [Test]
        public void EveryTuesdayAt0730FromMonday0630()
        {
            var expected = new DateTime(2015, 10, 6, 7, 30, 0);
            var from = new DateTime(2015, 10, 5, 6, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Tuesday, new Timestamp(7, 30), new From(from)).Evaluate());
        }

        [Test]
        public void EveryMondayAt0730FromTuesday0630()
        {
            var expected = new DateTime(2015, 10, 12, 7, 30, 0);
            var from = new DateTime(2015, 10, 6, 6, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Monday, new Timestamp(7, 30), new From(from)).Evaluate());

        }

        [Test]
        public void EveryMondayAt0630FromTuesday0730()
        {
            var expected = new DateTime(2015, 10, 12, 6, 30, 0);
            var from = new DateTime(2015, 10, 6, 7, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Monday, new Timestamp(6, 30), new From(from)).Evaluate());
        }

        [Test]
        public void EveryTuesdayAt0630FromMonday0630IncludeNowShouldStillGiveTuesdayAsFirstDay()
        {
            var expected = new DateTime(2015, 10, 6, 6, 30, 0);
            var from = new DateTime(2015, 10, 5, 6, 30, 0);
            Assert.AreEqual(expected, new Weekday(DayOfWeek.Tuesday, new Timestamp(6, 30), new From(from), true).Evaluate());
        }
    }
}
