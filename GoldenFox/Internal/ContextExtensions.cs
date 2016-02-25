using System.Linq;

using TestSomething;

namespace GoldenFox.Internal
{
    internal static class ContextExtensions
    {
        public static Timestamp ParseTime(this GoldenFoxLanguageParser.TimeContext node)
        {
            return new Timestamp(node.INT().Select(x => int.Parse(x.GetText())).ToArray());
        }
    }
}