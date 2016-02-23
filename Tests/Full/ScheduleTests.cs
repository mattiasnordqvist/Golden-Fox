using NUnit.Framework;

namespace Tests.Full
{
    [TestFixture]
    public class ScheduleTests
    {
        [Test]
        public void EveryHour()
        {
            "every hour".From("2015-01-01 05:23", true).Gives("2015-01-01 06:00");
        }

        [Test]
        public void EveryMinute()
        {
            "every minute".From("2015-01-01 05:23", false).Gives("2015-01-01 05:24");
            "every minute".From("2015-01-01 23:59", false).Gives("2015-01-02 00:00");
        }

        [Test]
        public void EveryMinuteBetweenBeforeStart()
        {
            "every minute between 16:00 and 17:00".From("2015-01-01 05:23", false).Gives("2015-01-01 16:00");
        }

        [Test]
        public void EveryMinuteBetweenBeforeInTheMiddle()
        {
            "every minute between 16:00 and 17:00".From("2015-01-01 16:23", false).Gives("2015-01-01 16:24");
        }

        [Test]
        public void EveryMinuteBetweenBeforeEnd()
        {
            "every minute between 16:00 and 17:00".From("2015-01-01 16:59", false).Gives("2015-01-01 17:00");
        }

        [Test]
        public void EveryMinuteBetweenAtEnd()
        {
            "every minute between 16:00 and 17:00".From("2015-01-01 17:00", false).Gives("2015-01-02 16:00");
        }

        [Test]
        public void EveryMinuteBetweenAfterEnd()
        {
            "every minute between 16:00 and 17:00".From("2015-01-01 17:10", false).Gives("2015-01-02 16:00");
        }

        [Test]
        public void EveryHourBetweenBeforeStart()
        {
            "every hour between 18:00 and 21:00".From("2015-01-01 05:23", false).Gives("2015-01-01 18:00");
        }

        [Test]
        public void EveryHourBetweenBeforeInTheMiddle()
        {
            "every hour between 18:00 and 21:00".From("2015-01-01 18:23", false).Gives("2015-01-01 19:00");
        }

        [Test]
        public void EveryHourBetweenBeforeEnd()
        {
            "every hour between 18:00 and 21:00".From("2015-01-01 20:59", false).Gives("2015-01-01 21:00");
        }

        [Test]
        public void EveryHourBetweenAtEnd()
        {
            "every hour between 18:00 and 21:00".From("2015-01-01 21:00", false).Gives("2015-01-02 18:00");
        }

        [Test]
        public void EveryHourBetweenAfterEnd()
        {
            "every hour between 18:00 and 21:00".From("2015-01-01 21:10", false).Gives("2015-01-02 18:00");
        }

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

        [Test]
        public void EverySecond()
        {
            "every second".From("2016-02-09").Gives("2016-02-09 00:00:01");
            "every second".From("2016-02-10 10:15:10").Gives("2016-02-10 10:15:11");
        }

        [Test]
        public void EverySecondWithIncludeNow()
        {
            "every second".From("2016-02-09", false).Gives("2016-02-09 00:00:01");
            "every second".From("2016-02-10", true).Gives("2016-02-10 00:00:00");
        }

        [Test]
        public void EverySecondBetween()
        {
            "every second between 16:10:20 and 16:10:40".From("2016-02-11 15:30:00").Gives("2016-02-11 16:10:20");
        }

        [Test]
        public void EveryMinuteAtSeconds()
        {
            "every minute at 15 seconds".From("2016-02-09").Gives("2016-02-09 00:00:15");
            "every minute @ 17 seconds".From("2016-02-09 11:14:18").Gives("2016-02-09 11:15:17");

            "every minute at mm:05".From("2016-02-09").Gives("2016-02-09 00:00:05");
            "every minute @ mm:06".From("2016-02-09 14:02:05").Gives("2016-02-09 14:02:06");

            "every minute at hh:mm:07".From("2016-02-09").Gives("2016-02-09 00:00:07");
            "every minute @ hh:mm:47".From("2016-02-09 15:47:30").Gives("2016-02-09 15:47:47");

            "every minute at 10s".From("2016-02-11 10:30:20").Gives("2016-02-11 10:31:10"); 
        }

        [Test]
        public void EveryMinuteAtSecondsWithIncludeNow()
        {
            "every minute @ 17 seconds".From("2016-02-09 11:14:17", false).Gives("2016-02-09 11:15:17");
            "every minute @ 17 seconds".From("2016-02-09 11:14:17", true).Gives("2016-02-09 11:14:17");
        }

        [Test]
        public void EveryMinuteAtSecondsAnd()
        {
            "every minute at 15 seconds and 45 seconds".From("2016-02-09 00:00:20").Gives("2016-02-09 00:00:45");
            "every minute @ 17 seconds and 27 seconds".From("2016-02-09 11:14:18").Gives("2016-02-09 11:14:27");

            "every minute at mm:05 and mm:55".From("2016-02-09 06:08:06").Gives("2016-02-09 06:08:55");
            "every minute @ mm:06 and mm:16".From("2016-02-09 14:02:15").Gives("2016-02-09 14:02:16");

            "every minute at hh:mm:07 and hh:mm:37".From("2016-02-09 00:00:10").Gives("2016-02-09 00:00:37");
            "every minute @ hh:mm:47 and hh:mm:57".From("2016-02-09 15:47:50").Gives("2016-02-09 15:47:57");
        }

        [Test]
        public void EveryMinuteAtSecondsBetween()
        {
            "every minute at 15 seconds between 12:00 and 14:00".From("2016-02-09 13:40:20").Gives("2016-02-09 13:41:15");
            "every minute @ 17 seconds between 13:30 and 14:00".From("2016-02-09 15:50:38").Gives("2016-02-10 13:30:17");

            "every minute at mm:05 between 18:40 and 19:00".From("2016-02-09").Gives("2016-02-09 18:40:05");
            "every minute @ mm:06 between 04:59 and 19:00".From("2016-02-09 06:17:00").Gives("2016-02-09 06:17:06");

            "every minute at hh:mm:07 between 10:11 and 16:00".From("2016-02-09").Gives("2016-02-09 10:11:07");
            "every minute @ hh:mm:47 between 09:00 and 14:00".From("2016-02-09 10:00:00").Gives("2016-02-09 10:00:47");
        }

        [Test]
        public void EveryHourAtMinutes()
        {
            "every hour at 10 minutes".From("2016-02-10").Gives("2016-02-10 00:10:00");
            "every hour @ 20 minutes".From("2016-02-10 03:55:00").Gives("2016-02-10 04:20:00");

            "every hour at hh:15".From("2016-02-10").Gives("2016-02-10 00:15:00");
            "every hour @ hh:25".From("2016-02-10 01:25:01").Gives("2016-02-10 02:25:00");

            "every hour at hh:15:30".From("2016-02-10 08:15:29").Gives("2016-02-10 08:15:30");
            "every hour @ hh:25:40".From("2016-02-10").Gives("2016-02-10 00:25:40");

            "every hour at 5m".From("2016-02-11").Gives("2016-02-11 00:05:00");
        }

        [Test]
        public void EveryHourAtMinutesWithIncludeNow()
        {
            "every hour @ 20 minutes".From("2016-02-10 03:20", false).Gives("2016-02-10 04:20:00");
            "every hour @ 20 minutes".From("2016-02-10 03:20", true).Gives("2016-02-10 03:20:00");
        }

        [Test]
        public void EveryHourAtMinutesAnd()
        {
            "every hour at 10 minutes and 15 minutes".From("2016-02-10").Gives("2016-02-10 00:10:00");
            "every hour @ 20 minutes and 25 minutes".From("2016-02-10 02:22:00").Gives("2016-02-10 02:25:00");

            "every hour at hh:30 and hh:35".From("2016-02-10 12:34:59").Gives("2016-02-10 12:35:00");
            "every hour @ hh:25 and hh:45".From("2016-02-10 14:45:01").Gives("2016-02-10 15:25:00");

            "every hour at hh:15:30 and hh:45:18".From("2016-02-10 07:30:05").Gives("2016-02-10 07:45:18");
            "every hour @ hh:25:40 and hh:55:30".From("2016-02-10 00:25:40").Gives("2016-02-10 00:55:30");
        }

        [Test]
        public void EveryHourAtMinutesBetween()
        {
            "every hour at 10 minutes between 12:00 and 18:00".From("2016-02-10").Gives("2016-02-10 12:10:00");
            "every hour @ 20 minutes between 13:00 and 19:00".From("2016-02-10 18:20:01").Gives("2016-02-11 13:20:00");

            "every hour at hh:15 between 14:00 and 20:00".From("2016-02-10").Gives("2016-02-10 14:15:00");
            "every hour @ hh:25 between 15:00 and 21:00".From("2016-02-10 15:25:01").Gives("2016-02-10 16:25:00");

            "every hour at hh:15:30 between 16:00 and 22:00".From("2016-02-10").Gives("2016-02-10 16:15:30");
            "every hour @ hh:25:40 between 17:00 and 23:00".From("2016-02-10 17:25:39").Gives("2016-02-10 17:25:40");
        }
    }
}