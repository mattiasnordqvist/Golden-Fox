using System;

using GoldenFox.Internal;

using NUnit.Framework;

namespace Tests.Internal
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void DaysOfYear_LeapYear()
        {
            Assert.AreEqual(366, new DateTime(2016, 1, 1).DaysOfYear());
        }

        [Test]
        public void DaysOfYear_NotLeapYear()
        {
            Assert.AreEqual(365, new DateTime(2017, 1, 1).DaysOfYear());
        }

        [Test]
        public void DaysOfMonth_LeapYearFeb()
        {
            Assert.AreEqual(29, new DateTime(2016, 2, 1).DaysOfMonth());
        }

        [Test]
        public void DaysOfMonth_NotLeapYearFeb()
        {
            Assert.AreEqual(28, new DateTime(2013, 2, 1).DaysOfMonth());
        }

        [Test]
        public void DaysOfMonth_Jan()
        {
            Assert.AreEqual(31, new DateTime(2014, 1, 9).DaysOfMonth());
        }

        [Test]
        public void StripMinutes()
        {
            Assert.AreEqual(new DateTime(2015, 1, 1, 13, 0,0), new DateTime(2015,1,1,13,45,12).StripMinutes());
        }
    }
}
