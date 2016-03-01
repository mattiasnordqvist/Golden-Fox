using System;
using System.Collections.Generic;
using System.Linq;

using GoldenFox.Internal.Constraints;
using GoldenFox.Internal.Operators;
using GoldenFox.Internal.Operators.Intervals;

using TestSomething;

namespace GoldenFox.Internal
{
    internal class OperatorBuilder : GoldenFoxLanguageBaseListener
    {
        private readonly Stack<Interval> _stack = new Stack<Interval>(); 

        private readonly Stack<Context> _contexts = new Stack<Context>();

        public IOperator<DateTime> Result { get; set; }

        private Context Current => _contexts.Peek();

        public override void EnterSchedule(GoldenFoxLanguageParser.ScheduleContext context)
        {
            _contexts.Push(new Context());
        }

        public override void ExitSchedule(GoldenFoxLanguageParser.ScheduleContext context)
        {
            while (_stack.Any())
            {
                Add(_stack.Pop());
            }

            _contexts.Pop();
        }

        public override void ExitSecondsOffset(GoldenFoxLanguageParser.SecondsOffsetContext context)
        {
            Current.SecondsOffset.Push(int.Parse(context.INT().GetText()));
        }

        public override void ExitMinutesOffset(GoldenFoxLanguageParser.MinutesOffsetContext context)
        {
            Current.SecondsOffset.Push(int.Parse(context.INT(0).GetText()) * 60 + (context.INT().Length == 2 ? int.Parse(context.INT(1).GetText()) : 0));
        }

        public override void ExitEveryday(GoldenFoxLanguageParser.EverydayContext context)
        {
            while (Current.Timestamps.Any())
            {
                _stack.Push(new Day(Current.Timestamps.Pop()));
            }
        }

        public override void ExitEverysecond(GoldenFoxLanguageParser.EverysecondContext context)
        {
            var second = new Second();
            while (Current.Constraints.Any())
            {
                second.AddConstraint(Current.Constraints.Pop());
            }

            _stack.Push(second);
        }

        public override void ExitEveryminute(GoldenFoxLanguageParser.EveryminuteContext context)
        {
            if (!Current.SecondsOffset.Any())
            {
                Current.SecondsOffset.Push(0);
            }
            
            while (Current.SecondsOffset.Any())
            {
                var min = new Minute { OffsetInSeconds = Current.SecondsOffset.Pop() };
                while (Current.Constraints.Any())
                {
                    min.AddConstraint(Current.Constraints.Pop());
                }

                _stack.Push(min);
            }

        }

        public override void ExitEveryweekday(GoldenFoxLanguageParser.EveryweekdayContext context)
        {
            var constraints = new List<IConstraint>();
            while (Current.Constraints.Any())
            {
                constraints.Add(Current.Constraints.Pop());
            }

            while (Current.Timestamps.Any())
            {
                var interval = new Weekday(ParseWeekDay(context.weekday()), Current.Timestamps.Pop());
                interval.AddConstraints(constraints);
                _stack.Push(interval);
            }
        }

        public override void ExitWeekdays(GoldenFoxLanguageParser.WeekdaysContext context)
        {
            var constraints = new List<IConstraint>();
            while (Current.Constraints.Any())
            {
                constraints.Add(Current.Constraints.Pop());
            }

            while (Current.Timestamps.Any())
            {
                var interval = new Weekday(ParseWeekDay(context.weekday()), Current.Timestamps.Pop());
                interval.AddConstraints(constraints);
                _stack.Push(interval);
            }
        }

        public override void ExitNumberedweekday(GoldenFoxLanguageParser.NumberedweekdayContext context)
        {
            var index = 0;
            if (context.numberedDay() != null)
            {
                index = int.Parse(context.numberedDay().INT().GetText());
                if (context.Last() != null)
                {
                    index = 7 - index + 1;
                }

                index = index % 7;
            }

            var constraints = new List<IConstraint>();
            while (Current.Constraints.Any())
            {
                constraints.Add(Current.Constraints.Pop());
            }

            while (Current.Timestamps.Any())
            {
                var interval = new Weekday((DayOfWeek)index, Current.Timestamps.Pop());
                interval.AddConstraints(constraints);
                _stack.Push(interval);
            }
        }

        public override void ExitNumbereddayinmonth(GoldenFoxLanguageParser.NumbereddayinmonthContext context)
        {
            var index = 0;
            if (context.numberedDay() != null)
            {
                index = int.Parse(context.numberedDay().INT().GetText());
                if (context.Last() != null)
                {
                    index = -index + 1;
                }
            }

            var constraints = new List<IConstraint>();
            while (Current.Constraints.Any())
            {
                constraints.Add(Current.Constraints.Pop());
            }

            while (Current.Timestamps.Any())
            {
                var interval = new DayInMonth(index, Current.Timestamps.Pop());
                interval.AddConstraints(constraints);
                _stack.Push(interval);
            }
        }

        public override void ExitNumbereddayinyear(GoldenFoxLanguageParser.NumbereddayinyearContext context)
        {
            var index = 0;
            if (context.numberedDay() != null)
            {
                index = int.Parse(context.numberedDay().INT().GetText());
                if (context.Last() != null)
                {
                    index = -index + 1;
                }
            }

            var constraints = new List<IConstraint>();
            while (Current.Constraints.Any())
            {
                constraints.Add(Current.Constraints.Pop());
            }

            while (Current.Timestamps.Any())
            {
                var interval = new DayInYear(index, Current.Timestamps.Pop());
                interval.AddConstraints(constraints);
                _stack.Push(interval);
            }
        }

        public override void ExitDate(GoldenFoxLanguageParser.DateContext context)
        {
            var date = new DateTime(int.Parse(context.INT(0).GetText()), int.Parse(context.INT(1).GetText()), int.Parse(context.INT(2).GetText()));
            Current.Dates.Push(date);
        }

        public override void ExitDatetime(GoldenFoxLanguageParser.DatetimeContext context)
        {
            var date = Current.Dates.Pop();

            ExtendedDateTime dateTime = new ExtendedDateTime();
            if (Current.Timestamps.Any())
            {
                dateTime.DateTime = date.SetTime(Current.Timestamps.Pop());
            }
            else
            {
                dateTime.DateTime = date;
                dateTime.TimeNotSpecified();
            }

            Current.DateTimes.Push(dateTime);
        }

        public override void ExitFrom(GoldenFoxLanguageParser.FromContext context)
        {
            var extDateTime = Current.DateTimes.Pop();
            var datetime = extDateTime.DateTime;
            var from = new From(datetime);
            _contexts.Pop();
            Current.Constraints.Push(from);
        }

        public override void ExitUntil(GoldenFoxLanguageParser.UntilContext context)
        {
            var extDateTime = Current.DateTimes.Pop();
            var datetime = extDateTime.DateTime;
            if (!extDateTime.TimeSpecified)
            {
                datetime = datetime.AddDays(1).AddSeconds(-1);
            }

            var until = new Until(datetime);
            _contexts.Pop();
            Current.Constraints.Push(until);
        }

        public override void ExitEveryhour(GoldenFoxLanguageParser.EveryhourContext context)
        {
            if (!Current.SecondsOffset.Any())
            {
                Current.SecondsOffset.Push(0);
            }

            while (Current.SecondsOffset.Any())
            {
                var hour = new Hour { OffsetInSeconds = Current.SecondsOffset.Pop() };
                while (Current.Constraints.Any())
                {
                    hour.AddConstraint(Current.Constraints.Pop());
                }

                _stack.Push(hour);
            }
        }

        public override void ExitBetween(GoldenFoxLanguageParser.BetweenContext context)
        {
            var second = Current.Timestamps.Pop();
            var first = Current.Timestamps.Pop();
            Current.Constraints.Push(new Between(first, second));
        }

        public override void ExitTime(GoldenFoxLanguageParser.TimeContext context)
        {
            Current.Timestamps.Push(context.ParseTime());
        }

        public void Add(IOperator<DateTime> op)
        {
            Result = Result != null ? new First(op, Result) : op;
        }

        public override void EnterFrom(GoldenFoxLanguageParser.FromContext context)
        {
            _contexts.Push(new Context());
        }

        public override void EnterUntil(GoldenFoxLanguageParser.UntilContext context)
        {
            _contexts.Push(new Context());
        }

        private DayOfWeek ParseWeekDay(GoldenFoxLanguageParser.WeekdayContext weekdayContext)
        {
            DayOfWeek dayOfWeek;
            Enum.TryParse(weekdayContext.GetText().Capitalize(), out dayOfWeek);
            return dayOfWeek;
        }
    }
}