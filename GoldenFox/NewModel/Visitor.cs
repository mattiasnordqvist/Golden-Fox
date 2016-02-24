using System;
using System.Collections.Generic;
using System.Linq;

using TestSomething;

namespace GoldenFox.NewModel
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
            var timeComponents = context.Time().GetText().Split(':').Select(int.Parse).ToArray();
            return new Weekday((DayOfWeek)index, new Timestamp(timeComponents));
        }

        public override IOperator<DateTime> VisitNumbereddayinmonth(GoldenFoxLanguageParser.NumbereddayinmonthContext context)
        {
            var index = 0;
            if (context.NumberedDay() != null)
            {
                index = int.Parse(context.NumberedDay().GetText().Substring(0, context.NumberedDay().GetText().Length-2).ToString());
                if (context.Last() != null)
                {
                    index = - index + 1;
                }
            }
            var timeComponents = context.Time().GetText().Split(':').Select(int.Parse).ToArray();
            return new DayInMonth(index, new Timestamp(timeComponents));
        }

        public override IOperator<DateTime> VisitWeekdays(GoldenFoxLanguageParser.WeekdaysContext context)
        {
            DayOfWeek dayOfWeek;
            Enum.TryParse(context.Weekday().GetText().Capitalize(), out dayOfWeek);
            var timeComponents = context.Time().GetText().Split(':').Select(int.Parse).ToArray();

            return new Weekday(dayOfWeek, new Timestamp(timeComponents));
        }

        public override IOperator<DateTime> VisitWeekday(GoldenFoxLanguageParser.WeekdayContext context)
        {
            DayOfWeek dayOfWeek;
            Enum.TryParse(context.Weekday().GetText().Capitalize(), out dayOfWeek);
            var timeComponents = context.Time().GetText().Split(':').Select(int.Parse).ToArray();
            
            return new Weekday(dayOfWeek, new Timestamp(timeComponents));
        }
    }
}