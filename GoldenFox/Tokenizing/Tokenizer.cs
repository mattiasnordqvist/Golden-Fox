using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.Tokenizing
{
    public class Tokenizer
    {
        protected List<IToken> _tokens = new List<IToken>();

        public Tokenizer(List<IToken> tokens)
        {
            _tokens = tokens;
        }

        public Tokenizer()
        {
        }

        public void AddToken(IToken token)
        {
            _tokens.Add(token);
        }

        public List<string> Parse(string s)
        {
            string left = s;
            var matches = new List<string>();
            var splitted = left.Split(' ');
            foreach (var st in splitted)
            {
                var std = st;
                while (std.Length > 0)
                {
                    var firstMatch = FindFirstMatch(std, _tokens);
                    matches.Add(firstMatch);
                    std = std.Substring(firstMatch.Length);
                }    
            }

            return matches;
        }

        private string FindFirstMatch(string text, List<IToken> allTokens)
        {
            var accumulated = string.Empty;
            var rest = text;
            var tokens = allTokens;
            var lastMatch = string.Empty;

            while (tokens.Any() && rest.Any())
            {
                var next = rest.First();
                accumulated = accumulated + next;
                rest = rest.Substring(1);
                tokens = Reduce(accumulated, allTokens);
                if (tokens.Any(x => x.Result == MatchResult.Yes))
                {
                    lastMatch = accumulated;
                }
            }

            if (lastMatch == string.Empty)
            {
                throw new TokenizationException(accumulated);
            }

            return lastMatch;
        }

        private List<IToken> Reduce(string s, List<IToken> candidates)
        {
            return candidates.Select(x => x.Match(s))
                .Where(x => x.Result == MatchResult.Yes || x.Result == MatchResult.Maybe)
                .ToList();
        }
    }
}
