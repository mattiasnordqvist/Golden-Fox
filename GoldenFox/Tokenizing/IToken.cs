namespace GoldenFox.Tokenizing
{
    public interface IToken
    {
        MatchResult Result { get; }
        IToken Match(string s);
    }
}
