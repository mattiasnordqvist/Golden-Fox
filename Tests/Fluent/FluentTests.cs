using System;

using GoldenFox.Fluent;
using GoldenFox.Internal;

using NUnit.Framework;

namespace Tests.Fluent
{
    [TestFixture]
    public class FluentTests
    {

        [Test]
        public void EverySecond()
        {
            var schedule = Every.Second();
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 0, 1), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 25, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 24, 59)));
        }

        [Test]
        public void EverySecondFrom()
        {
            var schedule = Every.Second().From(new DateTime(2015, 06, 01));
            Assert.AreEqual(new DateTime(2015, 06, 01, 0, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 06, 1, 16, 25, 0), schedule.Evaluate(new DateTime(2015, 6, 1, 16, 24, 59)));
        }

        [Test]
        public void EverySecondUntil()
        {
            var schedule = Every.Second().Until(new DateTime(2015, 06, 01));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 0, 1), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.Throws<InvalidOperationException>(() => schedule.Evaluate(new DateTime(2015, 6, 1, 16, 24, 59)));
        }

        [Test]
        public void EverySecondBetween()
        {
            var schedule = Every.Second().Between(new Timestamp(16)).And(new Timestamp(17));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 2, 16, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 18, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 2, 16, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 17, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 0, 1), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 25, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 24, 59)));
        }

        [Test]
        public void EverySecondFromBetween()
        {
            var schedule = Every.Second().Between(new Timestamp(16)).And(new Timestamp(17)).From(new DateTime(2015, 06, 01));
            Assert.AreEqual(new DateTime(2015, 06, 1, 16, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 06, 1, 16, 25, 0), schedule.Evaluate(new DateTime(2015, 6, 1, 16, 24, 59)));
        }


        [Test]
        public void EveryMinuteAndEveryMinuteWithGlobalBetween()
        {
            var schedule = 
                Every.Minute().WithOffset(15)
                .And(Every.Minute())
                .Between(new Timestamp(16)).And(new Timestamp(17));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 0, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 25, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 24, 59)));
            Assert.AreEqual(new DateTime(2015, 1, 2, 16, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 17, 24, 59)));
        }

        [Test]
        public void EveryMinuteAndEveryMinuteWithEqualLocalBetweens()
        {
            var schedule = 
                Every.Minute()
                    .WithOffset(15)
                    .Between(new Timestamp(16)).And(new Timestamp(17))
                .And(Every.Minute()
                    .Between(new Timestamp(16)).And(new Timestamp(17)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 0, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 25, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 24, 59)));
            Assert.AreEqual(new DateTime(2015, 1, 2, 16, 0, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 17, 24, 59)));
        }

        [Test]
        public void EveryMinute()
        {
            var schedule = Every.Minute();
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 1, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 25, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 24, 59)));
        }

        [Test]
        public void EveryMinuteAtOffset()
        {
            var schedule = Every.Minute().WithOffset(15);
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 0, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 1, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 30)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 25, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 24, 59)));
        }

        [Test]
        public void EveryMinuteAtTwoOffsets()
        {
            var schedule = Every.Minute().WithOffset(15).And(Every.Minute().WithOffset(45));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 0, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 0, 45), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 30)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 25, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 24, 59)));
        }

        [Test]
        public void EveryMinuteWithOffsetBetween()
        {
            var schedule = Every.Minute().WithOffset(15).And(Every.Minute().WithOffset(45));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 0, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 0, 45), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 30)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 25, 15), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 24, 59)));
        }

        [Test]
        public void EveryHourWithOffsetBetweenAndFrom()
        {
            var schedule = Every.Hour().WithOffset(30, 0).Between("15:00").And("17:00").From(new DateTime(2015, 1, 1));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 30, 0), schedule.Evaluate(new DateTime(2014, 1 ,1)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 30, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 0, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 15, 30, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 29, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 1, 16, 30, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 15, 30, 0)));
            Assert.AreEqual(new DateTime(2015, 1, 2, 15, 30, 0), schedule.Evaluate(new DateTime(2015, 1, 1, 16, 30, 0)));
        }

        [Test]
        public void DoubleUntilShouldWorkAsLongAsNotBothArePassed()
        {
            var schedule = Every.Hour().WithOffset(30, 0).Until(new DateTime(2015, 1, 1))
                .And(Every.Hour().WithOffset(45, 0).Until(new DateTime(2016, 1, 1)));
            Assert.AreEqual(new DateTime(2014, 1, 1, 0, 30, 0), schedule.Evaluate(new DateTime(2014, 1, 1)));
            Assert.AreEqual(new DateTime(2015, 2, 1, 0, 45, 0), schedule.Evaluate(new DateTime(2015, 2, 1)));
            Assert.Throws<InvalidOperationException>(() => schedule.Evaluate(new DateTime(2016, 2, 1)));
        }

    }
}
