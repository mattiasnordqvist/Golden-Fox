using System;
using System.Collections.Generic;
using System.Linq;

using TestSomething;

namespace GoldenFox.Internal
{
    internal class Listener : GoldenFoxLanguageBaseListener
    {
        private readonly Stack<object> _stack = new Stack<object>(); 

        private readonly Stack<Timestamp> _timestamps = new Stack<Timestamp>();

        private readonly List<Between> _betweens = new List<Between>();
        private readonly List<int> _secondsOffset = new List<int>();

        public IOperator<DateTime> Result { get; set; }

        public override void ExitSchedule(GoldenFoxLanguageParser.ScheduleContext context)
        {
            while (_stack.Any())
            {
                Add((IOperator<DateTime>)_stack.Pop());
            }
        }

        public override void ExitSecondsOffset(GoldenFoxLanguageParser.SecondsOffsetContext context)
        {
            _secondsOffset.Add(int.Parse(context.INT().GetText()));
        }

        public override void ExitMinutesOffset(GoldenFoxLanguageParser.MinutesOffsetContext context)
        {
            _secondsOffset.Add((int.Parse(context.INT(0).GetText()) * 60) + (context.INT().Length == 2 ? int.Parse(context.INT(1).GetText()) : 0));
        }

        public override void ExitEveryday(GoldenFoxLanguageParser.EverydayContext context)
        {
            while(_timestamps.Any())
            {
                _stack.Push(new Day(_timestamps.Pop()));
            }
        }


        public override void ExitEverysecond(GoldenFoxLanguageParser.EverysecondContext context)
        {
            var second = new Second();
            if (_betweens.Count == 1)
            {
                second.Between = _betweens.First();
            }

            _stack.Push(second);
            _betweens.Clear();
        }

        public override void ExitEveryminute(GoldenFoxLanguageParser.EveryminuteContext context)
        {
            if (!_secondsOffset.Any())
            {
                _secondsOffset.Add(0);
            }
            
            foreach (var secondOffset in _secondsOffset)
            {
                var min = new Minute { OffsetInSeconds = secondOffset };
                if (_betweens.Count == 1)
                {
                    min.Between = _betweens.First();
                }

                _stack.Push(min);
            }

            _betweens.Clear();
            _secondsOffset.Clear();
        }

        public override void ExitEveryweekday(GoldenFoxLanguageParser.EveryweekdayContext context)
        {
            while (_timestamps.Any())
            {
                _stack.Push(new Weekday(ParseWeekDay(context.weekday()), _timestamps.Pop()));
            }
        }

        public override void ExitWeekdays(GoldenFoxLanguageParser.WeekdaysContext context)
        {
            while (_timestamps.Any())
            {
                _stack.Push(new Weekday(ParseWeekDay(context.weekday()), _timestamps.Pop()));
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

            while (_timestamps.Any())
            {
                _stack.Push(new Weekday((DayOfWeek)index, _timestamps.Pop()));
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
            while (_timestamps.Any())
            {
                _stack.Push(new DayInMonth(index, _timestamps.Pop()));
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
            while (_timestamps.Any())
            {
                _stack.Push(new DayInYear(index, _timestamps.Pop()));
            }
        }

        public override void ExitEveryhour(GoldenFoxLanguageParser.EveryhourContext context)
        {
            if (!_secondsOffset.Any())
            {
                _secondsOffset.Add(0);
            }

            foreach (var secondOffset in _secondsOffset)
            {
                var hour = new Hour() { OffsetInSeconds = secondOffset };
                if (_betweens.Count == 1)
                {
                    hour.Between = _betweens.First();
                }

                _stack.Push(hour);
            }

            _betweens.Clear();
            _secondsOffset.Clear();
        }

        public override void ExitBetween(GoldenFoxLanguageParser.BetweenContext context)
        {
            var second = _timestamps.Pop();
            var first = _timestamps.Pop();
            _betweens.Add(new Between(first, second));
            _timestamps.Clear();
        }

        public override void ExitTime(GoldenFoxLanguageParser.TimeContext context)
        {
            _timestamps.Push(context.ParseTime());
        }

        public void Add(IOperator<DateTime> op)
        {
            Result = Result != null ? new First(op, Result) : op;
        }

        private DayOfWeek ParseWeekDay(GoldenFoxLanguageParser.WeekdayContext weekdayContext)
        {
            DayOfWeek dayOfWeek;
            Enum.TryParse(weekdayContext.GetText().Capitalize(), out dayOfWeek);
            return dayOfWeek;
        }
    }
}