using NUnit.Framework;

using Tests.Parsing;

namespace Tests.Full
{
    [TestFixture]
    public class ScheduleTests
    {
        [Test]
        public void EveryDayAt_ExactlyTheSameTime()
        {
            "every day at 06:30".From("2015-10-05 06:30:00", true).Gives("2015-10-05 06:30:00");
        }

        [Test]
        public void EveryDayAt_ExactlyTheSameTime_GivesNextDay()
        {
            "every day at 06:30".From("2015-10-05 06:30:00", false).Gives("2015-10-06 06:30:00");
        }

        [Test]
        public void EveryDayAt_ExactlyTheSameTime_atSignWorksToo()
        {
            "everyday@06:30".From("2015-10-05 06:30:00", true).Gives("2015-10-05 06:30:00");
        }

        [Test]
        public void EveryDayAt_SameDayJustLater()
        {
            "every day at 06:30".From("2015-10-05 05:20:00").Gives("2015-10-05 06:30:00");
        }

        [Test]
        public void EveryDayAt_SameDayJustEarlierMeansOneWeekLater()
        {
            "every day at 04:30".From("2015-10-05 05:10:00").Gives("2015-10-06 04:30:00");
        }

        [Test]
        public void _7thDayEveryWeekAtSpecificTime()
        {
            "7th day every week at 22:30".From("2015-08-14").Gives("2015-08-16 22:30:00");
        }

        [Test]
        public void LastDayEveryWeekAtSpecificTime()
        {
            "last day every week at 22:30".From("2015-08-14").Gives("2015-08-16 22:30:00");
        }

        [Test]
        public void LastDayEveryMonthAtSpecificTime()
        {
            "last day every month at 10:20".From("2015-08-14").Gives("2015-08-31 10:20:00");
        }

        [Test]
        public void LastDayEveryMonthAtSpecificTime_Test2_WhenWeNeedToSkipToNextMonth()
        {
            "last day every month at 10:20".From("2015-08-31 12:00:00").Gives("2015-09-30 10:20:00");
        }

        [Test]
        public void FirstDayEveryMonthAtSpecificTime()
        {
            "1st day every month at 10:20".From("2015-08-14").Gives("2015-09-01 10:20:00");
        }

        [Test]
        public void _2ndDayEveryMonthAtSpecificTime()
        {
            "2nd day every month at 10:20".From("2015-09-01").Gives("2015-09-02 10:20:00");
        }

        [Test]
        public void _2ndDayEveryMonthAtSpecificTime_WhenWeSkipToNewMonth()
        {
            "2nd day every month at 10:20".From("2015-08-14").Gives("2015-09-02 10:20:00");
        }

        [Test]
        public void SundaysAtSpecificTime()
        {
            "sundays at 22:30".From("2015-08-14").Gives("2015-08-16 22:30:00");
        }

        [Test]
        public void EverySundayAtSpecificTime()
        {
            "every sunday at 22:30".From("2015-08-14").Gives("2015-08-16 22:30:00");
        }

        [Test]
        public void _6thEveryWeekAtSpecificTime()
        {
            "6th day every week at 22:30".From("2015-08-14").Gives("2015-08-15 22:30:00");
        }

        [Test]
        public void _2ndLastDayEveryWeekAtSpecificTime()
        {
            "2nd last day every week at 22:30".From("2015-08-14").Gives("2015-08-15 22:30:00");
        }

        [Test]
        public void SaturdayEveryWeekAtSpecificTime()
        {
            "saturdays at 22:30".From("2015-08-14").Gives("2015-08-15 22:30:00");
        }

        [Test]
        public void EverySaturdayAtSpecificTime()
        {
            "every saturday at 22:30".From("2015-08-14").Gives("2015-08-15 22:30:00");
        }

        [Test]
        public void _5thEveryWeekAtSpecificTime()
        {
            "5th day every week at 22:30".From("2015-08-14").Gives("2015-08-14 22:30:00");
        }

        [Test]
        public void _3rdLastDayEveryWeekAtSpecificTime()
        {
            "3rd last day every week at 22:30".From("2015-08-14").Gives("2015-08-14 22:30:00");
        }

        [Test]
        public void FridaysAtSpecificTime()
        {
            "fridays at 22:30".From("2015-08-14").Gives("2015-08-14 22:30:00");
        }

        [Test]
        public void EveryFridayAtSpecificTime()
        {
            "every friday at 22:30".From("2015-08-14").Gives("2015-08-14 22:30:00");
        }

        [Test]
        public void _4thDayEveryWeekAtSpecificTime()
        {
            "4th day every week at 22:30".From("2015-08-14").Gives("2015-08-20 22:30:00");
        }

        [Test]
        public void _4thLastDayEveryWeekAtSpecificTime()
        {
            "4th last day every week at 22:30".From("2015-08-14").Gives("2015-08-20 22:30:00");
        }

        [Test]
        public void ThursdaysAtSpecificTime()
        {
            "thursdays at 22:30".From("2015-08-14").Gives("2015-08-20 22:30:00");
        }

        [Test]
        public void EveryThursdayAtSpecificTime()
        {
            "every thursday at 22:30".From("2015-08-14").Gives("2015-08-20 22:30:00");
        }

        [Test]
        public void _3rdDayEveryWeekAtSpecificTime()
        {
            "3rd day every week at 22:30".From("2015-08-14").Gives("2015-08-19 22:30:00");
        }

        [Test]
        public void _5thLastDayEveryWeekAtSpecificTime()
        {
            "5th last day every week at 22:30".From("2015-08-14").Gives("2015-08-19 22:30:00");
        }

        [Test]
        public void WednesdaysAtSpecificTime()
        {
            "wednesdays at 22:30".From("2015-08-14").Gives("2015-08-19 22:30:00");
        }

        [Test]
        public void EveryWednesdayAtSpecificTime()
        {
            "every wednesday at 22:30".From("2015-08-14").Gives("2015-08-19 22:30:00");
        }

        [Test]
        public void _2ndDayEveryWeekAtSpecificTime()
        {
            "2nd day every week at 22:30".From("2015-08-14").Gives("2015-08-18 22:30:00");
        }

        [Test]
        public void _6thLastDayEveryWeekAtSpecificTime()
        {
            "6th last day every week at 22:30".From("2015-08-14").Gives("2015-08-18 22:30:00");
        }

        [Test]
        public void TuesdaysAtSpecificTime()
        {
            "tuesdays at 22:30".From("2015-08-14").Gives("2015-08-18 22:30:00");
        }

        [Test]
        public void EveryTuesdayAtSpecificTime()
        {
            "every tuesday at 22:30".From("2015-08-14").Gives("2015-08-18 22:30:00");
        }

        [Test]
        public void _1stDayEveryWeekAtSpecificTime()
        {
            "1st day every week at 22:30".From("2015-08-14").Gives("2015-08-17 22:30:00");
        }

        [Test]
        public void _7thLastDayEveryWeekAtSpecificTime()
        {
            "7th last day every week at 22:30".From("2015-08-14").Gives("2015-08-17 22:30:00");
        }

        [Test]
        public void MondaysAtSpecificTime()
        {
            "mondays at 22:30".From("2015-08-14").Gives("2015-08-17 22:30:00");
        }

        [Test]
        public void EveryMondayAtSpecificTime()
        {
            "every monday at 22:30".From("2015-08-14").Gives("2015-08-17 22:30:00");
        }
    }
}