using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using GoldenFox.Tokenizing;
using GoldenFox.Tokenizing.TokenTypes;

namespace GoldenFox.Parsing
{
    public class ScheduleParser 
    {
        private readonly Tokenizer _tokenizer;

        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1116:SplitParametersMustStartOnLineAfterDeclaration", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed. Suppression is OK here."), SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "Reviewed. Suppression is OK here.")]
        public ScheduleParser()
        {
            _tokenizer = new Tokenizer();
            Add("every", "day", "month", "week",
                "at", "@", ":", 
                "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", 
                "last",
                "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday", 
                "s",
                "and");
            _tokenizer.AddToken(new IntegerToken());
        }

        public Schedule Parse(string parseThis)
        {
            return new Schedule(ParseParts(parseThis));
        }

        public List<string> ParseParts(string parseThis)
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