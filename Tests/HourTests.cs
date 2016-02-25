using System;

using GoldenFox.Internal;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class HourTests
    {
        [Test]
        public void HourWithOffsetInMinutesAndSecondsFromBefore()
        {
            var expected = new DateTime(2016, 2, 10, 8, 15, 30);
            var from = new DateTime(2016, 2, 10, 8, 15, 29);
            var sut = new Hour { OffsetInSeconds = (15 * 60) + 30 };
            Assert.AreEqual(expected, sut.Evaluate(from));
        }

        [Test]
        public void HourWithOffsetInMinutesAndSecondsFromAfter()
        {
            var expected = new DateTime(2016, 2, 10, 9, 15, 30);
            var from = new DateTime(2016, 2, 10, 8, 15, 31);
            var sut = new Hour { OffsetInSeconds = (15 * 60) + 30 };
            Assert.AreEqual(expected, sut.Evaluate(from));
        }

        [Test]
        public void HourWithOffsetInMinutesAndSecondsExactlySameTimeIncludeNow()
        {
            var expected = new DateTime(2016, 2, 10, 8, 15, 30);
            var from = new DateTime(2016, 2, 10, 8, 15, 30);
            var sut = new Hour { OffsetInSeconds = (15 * 60) + 30 };
            Assert.AreEqual(expected, sut.Evaluate(from, true));
        }

        [Test]
        public void HourWithOffsetInMinutesAndSecondsExactlySameTimeNotIncludeNow()
        {
            var expected = new DateTime(2016, 2, 10, 9, 15, 30);
            var from = new DateTime(2016, 2, 10, 8, 15, 30);
            var sut = new Hour { OffsetInSeconds = (15 * 60) + 30 };
            Assert.AreEqual(expected, sut.Evaluate(from, false));
        }

        [Test]
        public void JustHourNotIncludeNow()
        {
            var expected = new DateTime(2016, 2, 10, 9, 0, 0);
            var from = new DateTime(2016, 2, 10, 8, 15, 30);
            var sut = new Hour { };
            Assert.AreEqual(expected, sut.Evaluate(from, false));
        }

        [Test]
        public void JustHourIncludeNow()
        {
            var expected = new DateTime(2016, 2, 10, 9, 0, 0);
            var from = new DateTime(2016, 2, 10, 8, 15, 30);
            var sut = new Hour { };
            Assert.AreEqual(expected, sut.Evaluate(from, true));
        }

        [Test]
        public void JustHourIncludeNowExactSameTime()
        {
            var expected = new DateTime(2016, 2, 10, 9, 0, 0);
            var from = new DateTime(2016, 2, 10, 9, 0, 0);
            var sut = new Hour { };
            Assert.AreEqual(expected, sut.Evaluate(from, true));
        }
    }
}
