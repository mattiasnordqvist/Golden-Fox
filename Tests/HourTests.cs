using System;

using GoldenFox.Internal;
using GoldenFox.Internal.Operators.Intervals;

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
        public void HourWithOffsetInMinutesAndSecondsExactlySameTimeInclusive()
        {
            var expected = new DateTime(2016, 2, 10, 8, 15, 30);
            var from = new DateTime(2016, 2, 10, 8, 15, 30);
            var sut = new Hour { OffsetInSeconds = (15 * 60) + 30 };
            Assert.AreEqual(expected, sut.Evaluate(from, true));
        }

        [Test]
        public void HourWithOffsetInMinutesAndSecondsExactlySameTimeNotInclusive()
        {
            var expected = new DateTime(2016, 2, 10, 9, 15, 30);
            var from = new DateTime(2016, 2, 10, 8, 15, 30);
            var sut = new Hour { OffsetInSeconds = (15 * 60) + 30 };
            Assert.AreEqual(expected, sut.Evaluate(from, false));
        }

        [Test]
        public void JustHourNotInclusive()
        {
            var expected = new DateTime(2016, 2, 10, 9, 0, 0);
            var from = new DateTime(2016, 2, 10, 8, 15, 30);
            var sut = new Hour { };
            Assert.AreEqual(expected, sut.Evaluate(from, false));
        }

        [Test]
        public void JustHourInclusive()
        {
            var expected = new DateTime(2016, 2, 10, 9, 0, 0);
            var from = new DateTime(2016, 2, 10, 8, 15, 30);
            var sut = new Hour { };
            Assert.AreEqual(expected, sut.Evaluate(from, true));
        }

        [Test]
        public void JustHourInclusiveExactSameTime()
        {
            var expected = new DateTime(2016, 2, 10, 9, 0, 0);
            var from = new DateTime(2016, 2, 10, 9, 0, 0);
            var sut = new Hour { };
            Assert.AreEqual(expected, sut.Evaluate(from, true));
        }
    }
}
