using System;

using GoldenFox.Internal;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EveryYearTests
    {
        [Test]
        public void FirstDayEveryYearAt0630()
        {
            var expected = new DateTime(2016, 1, 1, 06, 30, 00);
            var from = new DateTime(2015, 09, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInYear(1, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void FirstDayEveryYearAt0630SkipToNextYear()
        {
            var expected = new DateTime(2016, 01, 01, 06, 30, 00);
            var from = new DateTime(2015, 12, 19, 12, 00, 00);
            Assert.AreEqual(expected, new DayInYear(1, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void SecondDayEveryYearAt0630()
        {
            var expected = new DateTime(2016, 1, 2, 06, 30, 00);
            var from = new DateTime(2015, 09, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInYear(2, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void LastDayEveryYearAt0630()
        {
            var expected = new DateTime(2015, 12, 31, 06, 30, 00);
            var from = new DateTime(2015, 09, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInYear(0, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void SecondLastDayEveryYearAt0630()
        {
            var expected = new DateTime(2015, 12, 30, 06, 30, 00);
            var from = new DateTime(2015, 09, 05, 12, 00, 00);
            Assert.AreEqual(expected, new DayInYear(-1, new Timestamp(6, 30)).Evaluate(from));
        }

        [Test]
        public void ExactlySameDayInclusiveStaysInSameYear()
        {
            var expected = new DateTime(2015, 02, 27, 06, 30, 00);
            var from = new DateTime(2015, 02, 27, 06, 30, 00);
            Assert.AreEqual(expected, new DayInYear(58, new Timestamp(6, 30)).Evaluate(from, true));
        }

        [Test]
        public void ExactlySameDayDoNotInclusiveJumpsToNextYear()
        {
            var expected = new DateTime(2016, 02, 27, 06, 30, 00);
            var from = new DateTime(2015, 02, 27, 06, 30, 00);
            Assert.AreEqual(expected, new DayInYear(58, new Timestamp(6, 30)).Evaluate(from));
        }
    }
}