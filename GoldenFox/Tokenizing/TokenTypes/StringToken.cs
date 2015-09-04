namespace GoldenFox.Tokenizing.TokenTypes
{
    public class StringToken : Token
    {
        private readonly string _s;

        public StringToken(string s)
        {
            _s = s;
        }

        public override MatchResult TryMatch(string s)
        {
            if (s == _s)
            {
                return MatchResult.Yes;
            }
            else if (_s.StartsWith(s))
            {
                return MatchResult.Maybe;
            }
            else
            {
                return MatchResult.No;
            }
        }
    }
}