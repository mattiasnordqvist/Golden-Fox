using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Antlr4.Runtime;

using GoldenFox.Internal;

using TestSomething;

namespace GoldenFox
{
    public class Fox : IEnumerable<DateTime>
    {
        private DateTime _current;
        private readonly IOperator<DateTime> _schedule;

        public Fox(string schedule, DateTime startFrom)
        {
            _schedule = Compile(schedule);
            _current = startFrom;
        }

        public static IOperator<DateTime> Compile(string schedule)
        {
            var lexer = new GoldenFoxLanguageLexer(new AntlrInputStream(new StringReader(schedule)));
            var tokens = new CommonTokenStream(lexer);
            var parser = new GoldenFoxLanguageParser(tokens);
            var tree = parser.schedule();
            var visitor = new Visitor();
            var interval = visitor.Visit(tree);
            return interval;
            
        } 

        public IEnumerator<DateTime> GetEnumerator()
        {
            var includeNow = true;
            while (true)
            {
                _current = _schedule.Evaluate(_current, includeNow);
                yield return _current;
                includeNow = false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}