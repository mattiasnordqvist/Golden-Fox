namespace GoldenFox.Tokenizing.TokenTypes
{
    public abstract class Token : IToken
    {
        public MatchResult Result { get; private set; }

        public abstract MatchResult TryMatch(string s);

        public IToken Match(string s)
        {
            Result = TryMatch(s);
            return this;
        }
    }
}