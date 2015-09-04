using System;

using GoldenFox.Parsing;

using NUnit.Framework;

namespace Tests.Parsing
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void TestMondayEquals1()
        {
            Assert.AreEqual(1, DateTimeExtensions.WeekdayAsInt("monday"));
        }

        [Test]
        public void TestSundayEquals7()
        {
            Assert.AreEqual(7, DateTimeExtensions.WeekdayAsInt("sunday"));
        }

        [Test]
        public void DaysTo0()
        {
            Assert.AreEqual(0, DateTime.Parse("2015-09-03").DaysTo("thursday"));
        }

        [Test]
        public void DaysTo1()
        {
            Assert.AreEqual(1, DateTime.Parse("2015-09-03").DaysTo("friday"));
        }

        [Test]
        public void DaysTo6()
        {
            Assert.AreEqual(6, DateTime.Parse("2015-09-03").DaysTo("wednesday"));
        }
    }
}