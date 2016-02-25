using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

using GoldenFox.Internal;

using TestSomething;

namespace GoldenFox
{
    public class Fox : IEnumerable<DateTime>
    {
        private readonly IOperator<DateTime> _schedule;

        private DateTime _current;

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

            var listener = new Listener();
            var walker = new ParseTreeWalker();

            walker.Walk(listener, tree);
            return listener.Result;
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