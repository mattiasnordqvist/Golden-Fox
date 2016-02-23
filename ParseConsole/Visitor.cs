using System;
using System.Collections.Generic;
using System.Linq;

using GoldenFox.NewModel;

using TestSomething;

namespace ParseConsole
{
    public class Visitor : GoldenFoxLanguageBaseVisitor<IOperator<DateTime>>
    {
        public override IOperator<DateTime> VisitInterval(GoldenFoxLanguageParser.IntervalContext context)
        {
            return VisitChildren(context);
        }

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

        public override IOperator<DateTime> VisitDay(GoldenFoxLanguageParser.DayContext context)
        {
            var timeComponents = context.Time().GetText().Split(':').Select(int.Parse).ToArray();
            return new Day(new Timestamp(timeComponents));
        }

        public override IOperator<DateTime> VisitWeekday(GoldenFoxLanguageParser.WeekdayContext context)
        {
            DayOfWeek dayOfWeek;
            Enum.TryParse(context.Weekday().GetText().Capitalize(), out dayOfWeek);
            var timeComponents = context.Time().GetText().Split(':').Select(int.Parse).ToArray();
            
            return new Weekday(dayOfWeek, new Timestamp(timeComponents));
        }
    }

    public static class StringExt
    {
        public static string Capitalize(this string @this)
        {
            return @this.Substring(0, 1).ToUpper() + @this.Substring(1).ToLower();
        }
    }
}