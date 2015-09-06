using System;

using GoldenFox.New;

using NUnit.Framework;

namespace Tests.New
{
    [TestFixture]
    public class ClockTest
    {
        [Test]
        public void Same()
        {
            var now = DateTime.Now;
            Assert.AreEqual(
                0,
                new Clock(now).CompareTo(new Clock(now)));
        }

        [Test]
        public void Later()
        {
            var now = DateTime.Now;
            Assert.AreEqual(
                1,
                Math.Sign(new Clock(now.AddHours(1)).CompareTo(new Clock(now))));
        }

        [Test]
        public void Earlier()
        {
            var now = DateTime.Now;
            Assert.AreEqual(
                -1,
                Math.Sign(new Clock(now.AddHours(-2)).CompareTo(new Clock(now))));
        }
    }
}