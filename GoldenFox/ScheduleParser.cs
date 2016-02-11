using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using GoldenFox.Tokenizing;
using GoldenFox.Tokenizing.TokenTypes;

namespace GoldenFox
{
    public class ScheduleParser
    {
        private class IntervalToUseForHours
        {
            public int Minutes { get; private set; }
            public int Seconds { get; private set; }

            public IntervalToUseForHours(int minutes, int seconds)
            {
                Minutes = minutes;
                Seconds = seconds;
            }
        }

        private readonly Tokenizer _tokenizer;

        private readonly string[] _days = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1116:SplitParametersMustStartOnLineAfterDeclaration", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "Reviewed. Suppression is OK here.")]
        public ScheduleParser()
        {
            _tokenizer = new Tokenizer();
            Add("every", "day", "month", "week", "year", "hour", 
                "minute", "minutes", "m,", "second", "seconds",
                "at", "@", ":",
                "st", "nd", "rd", "th",
                "last",
                "s", "m",
                "and",
                "between",
                "hh:mm:", "hh:", "mm:");
            Add(_days);
            _tokenizer.AddToken(new IntegerToken());
        }

        public Schedule Parse(string parseThis)
        {
            var schedule = new Schedule();

            var parts = new PartsTraverser(ParseParts(parseThis));

            ParseBigLoop(parts, schedule);

            return schedule;
        }

        private void ParseBigLoop(PartsTraverser parts, Schedule schedule)
        {
            var days = new List<Tuple<int, Interval>>();
            do
            {
                if (parts.NextIf("every"))
                {
                    if (parts.NextIf("day") || parts.Peek("hour") || parts.Peek("minute") || parts.Peek("second"))
                    {
                        days.AddRange(
                            new[]
                                {
                                    Tuple.Create(0, Interval.Week), Tuple.Create(1, Interval.Week),
                                    Tuple.Create(2, Interval.Week), Tuple.Create(3, Interval.Week),
                                    Tuple.Create(4, Interval.Week), Tuple.Create(5, Interval.Week),
                                    Tuple.Create(6, Interval.Week)
                                });
                    }
                    else 
                    {
                        // weekday
                        do
                        {
                            var nextDay = parts.NextPart();
                            days.Add(Tuple.Create(Array.IndexOf(_days, nextDay), Interval.Week));
                        }
                        while (parts.NextIf("and"));
                    }
                }
                else if (_days.Any(x => x == parts.Peek()))
                {
                    do
                    {
                        var nextDay = parts.NextPart();
                        days.Add(Tuple.Create(Array.IndexOf(_days, nextDay), Interval.Week));
                        parts.SkipAnyOrFail("s");
                    }
                    while (parts.NextIf("and"));
                }
                else if (IsNumeric(parts.Peek()) || parts.Peek("last"))
                {
                    var scopeDays = new List<int>();
                    do
                    {
                        if (parts.NextIf("last"))
                        {
                            scopeDays.Add(-1);
                        }
                        else
                        {
                            var nth = int.Parse(parts.NextPart()) - 1;
                            parts.SkipIf("st", "nd", "rd", "th");
                            if (parts.NextIf("last"))
                            {
                                nth = -1 - nth;
                            }

                            scopeDays.Add(nth);
                        }
                    }
                    while (parts.NextIf("and"));
                    parts.SkipAnyOrFail("day");
                    parts.SkipAnyOrFail("every");
                    if (parts.NextIf("week"))
                    {
                        scopeDays.ForEach(x => days.Add(Tuple.Create(x, Interval.Week)));
                    }
                    else if (parts.NextIf("month"))
                    {
                        scopeDays.ForEach(x => days.Add(Tuple.Create(x, Interval.Month)));
                    }
                    else if (parts.NextIf("year"))
                    {
                        scopeDays.ForEach(x => days.Add(Tuple.Create(x, Interval.Year)));
                    }
                    else
                    {
                        throw new ParsingException("Unexpected token " + parts.Peek() + ". Expected week, month or year.");
                    }
                }
            }
            while (parts.NextIf("and"));
            days = days.Distinct().ToList();

            schedule.Times = ParseTimes(parts);
            schedule.Days = days;
        }

        private List<Clock> ParseTimes(PartsTraverser parts)
        {
            Clock from = null;
            Clock to = null;

            var times = new List<Clock>();
            if (parts.SkipIf("hour"))
            {
                var intervals = new List<IntervalToUseForHours>();

                while (parts.SkipIf("@", "at", "and"))
                {
                    if (IsNumeric(parts.Peek()))
                    {
                        intervals.Add(
                            new IntervalToUseForHours(
                                int.Parse(parts.NextPart()),
                                -1));
                        parts.SkipAnyOrFail("minutes", "m");
                    }
                    else if (parts.NextIf("hh:"))
                    {
                        var seconds = -1;
                        var minutes = int.Parse(parts.NextPart());
                        if (parts.SkipIf(":"))
                        {
                            seconds = int.Parse(parts.NextPart());
                        }

                        intervals.Add(
                            new IntervalToUseForHours(
                                minutes,
                                seconds));
                    }
                    else
                    {
                        throw new ParsingException("Unexpected token " + parts.Peek() +
                                                   ". Expected \"hh:mm:\" or \"mm:\".");
                    }
                }

                //times = Enumerable.Range(0, 24).Select(x => new Clock(x, 0)).ToList();
                if (parts.SkipIf("between"))
                {
                    from = ParseClock(parts);
                    parts.SkipAnyOrFail("and");
                    to = ParseClock(parts);
                }

                for (var hh = 0; hh < 24; hh++)
                {
                    for (var mm = 0; mm < 60; mm++)
                    {
                        for (var ss = 0; ss < 60; ss++)
                        {
                            var clock = new Clock(hh, mm, ss);
                            if (from != null && to != null)
                            {
                                if (clock.CompareTo(from) >= 0 && clock.CompareTo(to) <= 0)
                                {
                                    if (intervals.Any())
                                    {
                                        if (intervals.Any(i =>
                                            ((i.Minutes >= 0 && i.Seconds >= 0) && (clock.Minute == i.Minutes && clock.Seconds == i.Seconds)) ||
                                            ((i.Minutes >= 0 && i.Seconds < 0) && (clock.Minute == i.Minutes && clock.Seconds == 0))))
                                        {
                                            times.Add(clock);
                                        }
                                    }
                                    else
                                    {
                                        if (clock.Minute == 0 && clock.Seconds == 0)
                                        {
                                            times.Add(clock);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (intervals.Any())
                                {
                                    if (intervals.Any(i =>
                                        ((i.Minutes >= 0 && i.Seconds >= 0) && (clock.Minute == i.Minutes && clock.Seconds == i.Seconds)) ||
                                        ((i.Minutes >= 0 && i.Seconds < 0) && (clock.Minute == i.Minutes && clock.Seconds == 0))))
                                    {
                                        times.Add(clock);
                                    }
                                }
                                else
                                {
                                    if (clock.Minute == 0 && clock.Seconds == 0)
                                    {
                                        times.Add(clock);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (parts.SkipIf("minute"))
            {
                var secondsIntervals = new List<int>();

                while (parts.SkipIf("@", "at", "and"))
                {
                    if (IsNumeric(parts.Peek()))
                    {
                        secondsIntervals.Add(int.Parse(parts.NextPart()));
                        parts.SkipAnyOrFail("seconds", "s");
                    }
                    else if (parts.NextIf("hh:mm:"))
                    {
                        if (IsNumeric(parts.Peek()))
                        {
                            secondsIntervals.Add(int.Parse(parts.NextPart()));
                        }
                    }
                    else if (parts.NextIf("mm:"))
                    {
                        if (IsNumeric(parts.Peek()))
                        {
                            secondsIntervals.Add(int.Parse(parts.NextPart()));
                        }
                    }
                    else
                    {
                        throw new ParsingException("Unexpected token " + parts.Peek() +
                                                   ". Expected \"hh:mm:\" or \"mm:\".");
                    }
                }

                if (parts.SkipIf("between"))
                {
                    from = ParseClock(parts);
                    parts.SkipAnyOrFail("and");
                    to = ParseClock(parts);
                }

                for (var hh = 0; hh < 24; hh++)
                {
                    for (var mm = 0; mm < 60; mm++)
                    {
                        for (var ss = 0; ss < 60; ss++)
                        {
                            var clock = new Clock(hh, mm, ss);
                            if (from != null && to != null)
                            {
                                if (clock.CompareTo(from) >= 0 && clock.CompareTo(to) <= 0)
                                {
                                    if (secondsIntervals.Any())
                                    {
                                        if (secondsIntervals.Contains(clock.Seconds))
                                        {
                                            times.Add(clock);
                                        }
                                    }
                                    else
                                    {
                                        if (clock.Seconds == 0)
                                        {
                                            times.Add(clock);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (secondsIntervals.Any())
                                {
                                    if (secondsIntervals.Contains(clock.Seconds))
                                    {
                                        times.Add(clock);
                                    }
                                }
                                else if (clock.Seconds == 0)
                                {
                                    times.Add(clock);
                                }
                            }
                        }
                    }
                }
            }
            else if (parts.SkipIf("second"))
            {
                if (parts.SkipIf("between"))
                {
                    from = ParseClock(parts);
                    parts.SkipAnyOrFail("and");
                    to = ParseClock(parts);
                }

                for (var hh = 0; hh < 24; hh++)
                {
                    for (var mm = 0; mm < 60; mm++)
                    {
                        for (var ss = 0; ss < 60; ss++)
                        {
                            if (hh == 16 && mm == 10)
                            {
                                var stop = 0;
                            }
                            var clock = new Clock(hh, mm, ss);
                            if (from != null && to != null)
                            {
                                if (clock.CompareTo(from) >= 0 && clock.CompareTo(to) <= 0)
                                {
                                    times.Add(clock);
                                }
                            }
                            else
                            {
                                times.Add(clock);
                            }
                        }
                    }
                }
            }
            else
            {
                parts.SkipAnyOrFail("@", "at");

                do
                {
                    times.Add(ParseClock(parts));
                } while (parts.NextIf("and"));
            }

            return times;
        } 

        private Clock ParseClock(PartsTraverser parts)
        {
            var part = parts.NextPart();
            var hour = int.Parse(part);
            parts.SkipAnyOrFail(":");
            part = parts.NextPart();
            var minute = int.Parse(part);
            if (parts.SkipIf(":"))
            {
                var seconds = int.Parse(parts.NextPart());
                return new Clock { Hour = hour, Minute = minute, Seconds = seconds};
            }

            return new Clock { Hour = hour, Minute = minute };
        }

        private List<string> ParseParts(string parseThis)
        {
            return _tokenizer.Parse(parseThis);
        }

        private void Add(params string[] empty)
        {
            foreach (var s in empty)
            {
                _tokenizer.AddToken(new StringToken(s));
            }
        }

        private bool IsNumeric(string s)
        {
            int n;
            var isNumeric = int.TryParse(s, out n);
            return isNumeric;
        }
    }
}