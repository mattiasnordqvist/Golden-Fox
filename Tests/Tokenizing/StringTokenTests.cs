using GoldenFox.Tokenizing;
using GoldenFox.Tokenizing.TokenTypes;

using NUnit.Framework;

namespace Tests.Tokenizing
{
    [TestFixture]
    public class StringTokenTests
    {
        private IToken _test = new StringToken("test");

        [Test]
        public void ExactMatch()
        {
            Assert.AreEqual(MatchResult.Yes, _test.Match("test").Result);
        }

        [Test]
        public void MayMatch()
        {
            Assert.AreEqual(MatchResult.Maybe, _test.Match("t").Result);
        }

        [Test]
        public void NoMatch()
        {
            Assert.AreEqual(MatchResult.No, _test.Match("hest").Result);
        }
    }
}