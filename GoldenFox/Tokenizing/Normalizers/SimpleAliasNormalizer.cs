using System.Collections.Generic;
using System.Linq;

namespace GoldenFox.Tokenizing.Normalizers
{
    public class AtAliasNormalizer : SimpleAliasNormalizer
    {
        public AtAliasNormalizer()
        {
            From = "at";
            To = "@";
        }
    }

    public abstract class SimpleAliasNormalizer : INormalizer
    {
        protected string From { get; set; }
        protected string To { get; set; }
        public List<string> Normalize(List<string> parts)
        {
            return parts.Select(x => (x == From) ? To : x).ToList();
        }
    }

    internal interface INormalizer
    {
        List<string> Normalize(List<string> parts);
    }
}
