using System;

using GoldenFox.Internal;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EveryMonthsTests
    {
        [Test]
        public void FirstDayEveryMonthAt0630()
        {
            var expected = new DateTime(2015, 10, 01, 06, 30, 00);
            var from = new DateTime(2015, 09, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInMonth(1, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void FirstDayEveryMonthAt0630SkipToNextYear()
        {
            var expected = new DateTime(2016, 01, 01, 06, 30, 00);
            var from = new DateTime(2015, 12, 19, 12, 00, 00);
            Assert.AreEqual(expected, new DayInMonth(1, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void SecondDayEveryMonthAt0630()
        {
            var expected = new DateTime(2015, 10, 02, 06, 30, 00);
            var from = new DateTime(2015, 09, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInMonth(2, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void SecondDayEveryMonthAt0630JumpJustOneDay()
        {
            var expected = new DateTime(2015, 10, 02, 06, 30, 00);
            var from = new DateTime(2015, 10, 01, 12, 00, 00);
            Assert.AreEqual(expected, new DayInMonth(2, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void LastDayEveryMonthAt0630()
        {
            var expected = new DateTime(2015, 09, 30, 06, 30, 00);
            var from = new DateTime(2015, 09, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInMonth(0, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void SecondLastDayEveryMonthAt0630()
        {
            var expected = new DateTime(2015, 09, 29, 06, 30, 00);
            var from = new DateTime(2015, 09, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInMonth(-1, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void SecondLastDayEveryMonthAt0630TestFebruary()
        {
            var expected = new DateTime(2015, 02, 27, 06, 30, 00);
            var from = new DateTime(2015, 02, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInMonth(-1, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void ExactlySameDayInclusiveStaysInSameMonth()
        {
            var expected = new DateTime(2015, 02, 27, 06, 30, 00);
            var from = new DateTime(2015, 02, 27, 06, 30, 00);
            Assert.AreEqual(expected, new DayInMonth(27, new Timestamp(6, 30)).Evaluate(from, true));
        }

        [Test]
        public void ExactlySameDayDoNotInclusiveJumpsToNextMonth()
        {
            var expected = new DateTime(2015, 03, 27, 06, 30, 00);
            var from = new DateTime(2015, 02, 27, 06, 30, 00);
            Assert.AreEqual(expected, new DayInMonth(27, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void SameDayFromJustAfterJumpsToNextMonth()
        {
            var expected = new DateTime(2015, 03, 27, 06, 30, 00);
            var from = new DateTime(2015, 02, 27, 06, 40, 00);
            Assert.AreEqual(expected, new DayInMonth(27, new Timestamp(6, 30)).Evaluate(from));
        }
    }
}