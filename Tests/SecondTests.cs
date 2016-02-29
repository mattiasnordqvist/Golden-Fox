using System;

using GoldenFox.Internal;
using GoldenFox.Internal.Constraints;
using GoldenFox.Internal.Operators.Intervals;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SecondTests
    {
        [Test]
        public void EverySecond()
        {
            var expected = new DateTime(2015, 1, 1, 5, 24, 12);
            var from = new DateTime(2015, 1, 1, 5, 24, 11);
            var sut = new Second();
            Assert.AreEqual(expected, sut.Evaluate(from));
        }

        [Test]
        public void EverySecondExactlyOutsideBetween()
        {
            var expected = new DateTime(2015, 1, 2, 12, 0, 0);
            var from = new DateTime(2015, 1, 1, 13, 0, 0);

            var sut = new Second();
            sut.AddConstraint(new Between(new Timestamp(12), new Timestamp(13)));
            Assert.AreEqual(expected, sut.Evaluate(from));
        }

        [Test]
        public void EverySecondExactlyOutsideBetweenButInclusive()
        {
            var expected = new DateTime(2015, 1, 1, 13, 0, 0);
            var from = new DateTime(2015, 1, 1, 13, 0, 0);

            var sut = new Second();
            sut.AddConstraint(new Between(new Timestamp(12), new Timestamp(13)));
            Assert.AreEqual(expected, sut.Evaluate(from, true));
        }
    }
}