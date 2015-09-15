using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using GoldenFox.Tokenizing;
using GoldenFox.Tokenizing.TokenTypes;

namespace GoldenFox.New
{
    public class ScheduleParser
    {
        private readonly Tokenizer _tokenizer;

        private readonly string[] _days = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
        private readonly string[] _nthWeekdays = { "1st", "2nd", "3rd", "4th", "5th", "6th", "7th" };

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1116:SplitParametersMustStartOnLineAfterDeclaration", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "Reviewed. Suppression is OK here.")]
        public ScheduleParser()
        {
            _tokenizer = new Tokenizer();
            Add("every", "day", "month", "week",
                "at", "@", ":",
                "1st", "2nd", "3rd", "4th", "5th", "6th", "7th",
                "last",
                "s",
                "and");
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
                    if (parts.NextIf("day"))
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
                else if (_nthWeekdays.Any(x => x == parts.Peek() || parts.Peek("last")))
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
                            var nth = Array.IndexOf(_nthWeekdays, parts.NextPart());
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
                        new ParsingException("Unexpected token " + parts.Peek() + ". Expected week, month or year.");
                    }
                }
            }
            while (parts.NextIf("and"));
            days = days.Distinct().ToList();

            parts.SkipAnyOrFail("@", "at");

            var times = new List<Clock>();
            do
            {
                var part = parts.NextPart();
                var hour = int.Parse(part);
                parts.SkipAnyOrFail(":");
                part = parts.NextPart();
                var minute = int.Parse(part);
                times.Add(new Clock { Hour = hour, Minute = minute });
            }
            while (parts.NextIf("and"));

            schedule.Times = times;
            schedule.Days = days;
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
    }
}