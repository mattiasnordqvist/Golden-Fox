using System;
using System.Collections.Generic;
using System.Linq;

using Antlr4.Runtime.Tree;

using TestSomething;

namespace GoldenFox.NewModel
{
    public class Visitor : GoldenFoxLanguageBaseVisitor<IOperator<DateTime>>
    {
        protected override IOperator<DateTime> AggregateResult(IOperator<DateTime> aggregate, IOperator<DateTime> nextResult)
        {
            if (aggregate == null)
            {
                return nextResult;
            }
            if (nextResult == null)
            {
                return aggregate;
            }
            return new First(new List<IOperator<DateTime>> {aggregate, nextResult});
        }

        public override IOperator<DateTime> VisitSchedule(GoldenFoxLanguageParser.ScheduleContext context)
        {
            return VisitChildren(context);
        }

        public override IOperator<DateTime> VisitHour(GoldenFoxLanguageParser.HourContext context)
        {
            var time0 = context.Time(0);
            var time1 = context.Time(1);
            if (time0 != null && time1 != null)
            {
                return new Hour(new Between(ParseTime(time0), ParseTime(time1)));
            }

            return new Hour();
        }

        public override IOperator<DateTime> VisitMinute(GoldenFoxLanguageParser.MinuteContext context)
        {
            var time0 = context.Time(0);
            var time1 = context.Time(1);
            if (time0 != null && time1 != null)
            {
                return new Minute(new Between(ParseTime(time0), ParseTime(time1)));
            }

            return new Minute();
        }

        public override IOperator<DateTime> VisitDay(GoldenFoxLanguageParser.DayContext context)
        {
            return new Day(ParseTime(context.Time()));
        }

        public override IOperator<DateTime> VisitNumberedweekday(GoldenFoxLanguageParser.NumberedweekdayContext context)
        {
            var index = 0;
            if (context.NumberedDay() != null)
            {
                index = int.Parse(context.NumberedDay().GetText()[0].ToString());
                if (context.Last() != null)
                {
                    index = 7 - index + 1;
                }

                index = index % 7;
            }
            return new Weekday((DayOfWeek)index, ParseTime(context.Time()));
        }

        private Timestamp ParseTime(ITerminalNode node)
        {
            return new Timestamp(node.GetText().Split(':').Select(int.Parse).ToArray());;
        }

        public override IOperator<DateTime> VisitNumbereddayinmonth(GoldenFoxLanguageParser.NumbereddayinmonthContext context)
        {
            var index = 0;
            if (context.NumberedDay() != null)
            {
                index = int.Parse(context.NumberedDay().GetText().Substring(0, context.NumberedDay().GetText().Length - 2));
                if (context.Last() != null)
                {
                    index = - index + 1;
                }
            }
            return new DayInMonth(index, ParseTime(context.Time()));
        }

        public override IOperator<DateTime> VisitWeekdays(GoldenFoxLanguageParser.WeekdaysContext context)
        {
            return new Weekday(ParseWeekDay(context.Weekday()), ParseTime(context.Time()));
        }

        public override IOperator<DateTime> VisitWeekday(GoldenFoxLanguageParser.WeekdayContext context)
        {
            return new Weekday(ParseWeekDay(context.Weekday()), ParseTime(context.Time()));
        }

        private DayOfWeek ParseWeekDay(ITerminalNode node)
        {
            DayOfWeek dayOfWeek;
            Enum.TryParse(node.GetText().Capitalize(), out dayOfWeek);
            return dayOfWeek;
        }
    }
}