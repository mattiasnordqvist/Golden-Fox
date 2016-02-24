namespace GoldenFox.NewModel
{
    public static class StringExt
    {
        public static string Capitalize(this string @this)
        {
            return @this.Substring(0, 1).ToUpper() + @this.Substring(1).ToLower();
        }
    }
}