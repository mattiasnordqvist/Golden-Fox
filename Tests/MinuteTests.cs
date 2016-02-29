using System;

using GoldenFox.Internal;
using GoldenFox.Internal.Operators.Intervals;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MinuteTests
    {
        [Test]
        public void EveryMinute()
        {
            var expected = new DateTime(2015, 1, 1, 5, 24, 0);
            var from = new DateTime(2015, 1, 1, 5, 23, 0);
            var sut = new Minute();
            Assert.AreEqual(expected, sut.Evaluate(from));
        }
    }
}