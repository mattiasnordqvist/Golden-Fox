namespace GoldenFox.Tokenizing.TokenTypes
{
    public class IntegerToken : Token
    {
        public override MatchResult TryMatch(string s)
        {
            int parsed;
            return int.TryParse(s, out parsed) ? MatchResult.Yes : MatchResult.No;
        }
    }
}