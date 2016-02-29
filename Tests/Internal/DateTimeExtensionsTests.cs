using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GoldenFox.Internal;

using NUnit.Framework.Internal;
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
    }
}
