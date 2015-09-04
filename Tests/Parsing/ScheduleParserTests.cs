using System.Collections.Generic;

using GoldenFox.Parsing;

using NUnit.Framework;

namespace Tests.Parsing
{
    [TestFixture]
    public class ScheduleParserTests
    {
        private readonly ScheduleParser _parser = new ScheduleParser();

        [Test]
        public void Simple()
        {
            CollectionAssert.AreEqual(new List<string> { "every", "day" }, _parser.Parse("every day").Parts);
        }
    }
}