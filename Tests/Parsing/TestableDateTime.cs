using System;

using NUnit.Framework;

namespace Tests.Parsing
{
    public class TestableDateTime
    {
        private readonly DateTime _from;
        private readonly DateTime _result;

        public TestableDateTime(DateTime from, DateTime result)
        {
            _from = @from;
            _result = result;
        }

        public void Gives(string expected)
        {
            var expectedDateTime = DateTime.Parse(expected);
            if (expectedDateTime < _from)
            {
                Assert.Fail("Golden Fox refuses to test this since the conditions are absurd. You're expecting " + expected + " to be before " + _from + "?");    
            }

            if (_result < _from)
            {
                Assert.Fail("Whatever you expected, Golden Fox seem confused, because he gave " + _result + " which is clearly not after " + _from);
            }

            Assert.AreEqual(expectedDateTime, _result);
        }
    }
}