using System.Collections.Generic;

using GoldenFox.Tokenizing;
using GoldenFox.Tokenizing.TokenTypes;

using NUnit.Framework;

namespace Tests.Tokenizing
{
    [TestFixture]
    public class TokenizerTests
    {
        [Test]
        public void SingleTokenAppearsOnce()
        {
            var tokens = new List<IToken> { new StringToken("test") };
            var parseThis = "test";
            var expected = new List<string> { "test" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void SingleTokenAppearsTwice()
        {
            var tokens = new List<IToken> { new StringToken("test") };
            var parseThis = "testtest";
            var expected = new List<string> { "test", "test" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void SingleTokenAppearsTwiceHandleSpaces()
        {
            var tokens = new List<IToken> { new StringToken("test") };
            var parseThis = " test test ";
            var expected = new List<string> { "test", "test" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void MultipleTokensAppearsOnceEach()
        {
            var tokens = new List<IToken> { new StringToken("test"), new StringToken("hej") };
            var parseThis = "hej test";
            var expected = new List<string> { "hej", "test" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void MultipleTokensAppearsMultipleTimes()
        {
            var tokens = new List<IToken> { new StringToken("test"), new StringToken("hej") };
            var parseThis = "hej testhejtest";
            var expected = new List<string> { "hej", "test", "hej", "test" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void MultipleTokensWithPartialMatches()
        {
            var tokens = new List<IToken> { new StringToken("hej"), new StringToken("hejsan") };
            var parseThis = "hej hejsan hej";
            var expected = new List<string> { "hej", "hejsan", "hej" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void MultipleTokensWithPartialMatches2()
        {
            var tokens = new List<IToken> { new StringToken("a"), new StringToken("ab"), new StringToken("abc"), new StringToken("abcd") };
            var parseThis = "aababcabcdabcaba";
            var expected = new List<string> { "a", "ab", "abc", "abcd", "abc", "ab", "a" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void SingleIntegerToken()
        {
            var tokens = new List<IToken> { new IntegerToken() };
            var parseThis = "1";
            var expected = new List<string> { "1" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void IntegerLongerThanOneChar()
        {
            var tokens = new List<IToken> { new IntegerToken() };
            var parseThis = "12";
            var expected = new List<string> { "12" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void TextAndIntegerRespectsSpace()
        {
            var tokens = new List<IToken> { new StringToken("test"), new IntegerToken() };
            var parseThis = "test 12";
            var expected = new List<string> { "test", "12" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void IntegerAndTextRespectsSpace()
        {
            var tokens = new List<IToken> { new StringToken("test"), new IntegerToken() };
            var parseThis = " 12 test ";
            var expected = new List<string> { "12", "test" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void SpaceBetweenIntegersSeperatesThem()
        {
            var tokens = new List<IToken> { new IntegerToken() };
            var parseThis = " 12 1 ";
            var expected = new List<string> { "12", "1" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void SpaceBetweenTextsSeperatesThem1()
        {
            var tokens = new List<IToken> { new StringToken("test"), new StringToken("testtest") };
            var parseThis = "test test";
            var expected = new List<string> { "test", "test" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void SpaceBetweenTextsSeperatesThem2()
        {
            var tokens = new List<IToken> { new StringToken("test"), new StringToken("testtest") };
            var parseThis = "testtest";
            var expected = new List<string> { "testtest" };
            var parsed = new Tokenizer(tokens).Parse(parseThis);
            CollectionAssert.AreEqual(expected, parsed);
        }

        [Test]
        public void ParseExceptionNoMatchTokens()
        {
            var tokens = new List<IToken>();
            var parseThis = "testtest";
            var tokenizer = new Tokenizer(tokens);
            Assert.Throws<TokenizationException>(() => tokenizer.Parse(parseThis));
        }
    }
}