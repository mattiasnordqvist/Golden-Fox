using System;

using GoldenFox.Internal;

using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TimestampTest
    {
        [Test]
        public void Same()
        {
            var now = DateTime.Now;
            Assert.AreEqual(
                0,
                new Timestamp(now).CompareTo(new Timestamp(now)));
        }

        [Test]
        public void Later()
        {
            var now = new DateTime(2015, 11, 1, 12, 1, 2);
            var later = now.AddHours(1);
            Assert.AreEqual(
                1,
                Math.Sign(new Timestamp(later).CompareTo(new Timestamp(now))));
        }

        [Test]
        public void Earlier()
        {
            var now = new DateTime(2015, 11, 1, 12, 1, 2);
            var earlier = now.AddHours(-2);
            Assert.AreEqual(
                -1,
                Math.Sign(new Timestamp(earlier).CompareTo(new Timestamp(now))));
        }
    }
}