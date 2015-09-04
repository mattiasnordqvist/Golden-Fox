namespace GoldenFox.Parsing
{
    public class TimeParser
    {
        private readonly PartsTraverser _partsTraverser;

        public TimeParser(PartsTraverser partsTraverser)
        {
            _partsTraverser = partsTraverser;
        }

        public Time Parse()
        {
            var part = Next();
            var hour = int.Parse(part);
            Next();
            part = Next();
            var minute = int.Parse(part);
            return new Time { Hour = hour, Minute = minute };
        }

        private string Next()
        {
            return _partsTraverser.NextPart();
        }
    }
}