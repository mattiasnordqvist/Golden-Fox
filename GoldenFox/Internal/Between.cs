namespace GoldenFox.Internal
{
    internal class Between
    {
        public Between(Timestamp from, Timestamp to)
        {
            From = @from;
            To = to;
        }

        public Timestamp From { get; set; }

        public Timestamp To { get; set; }

        public bool Contains(Timestamp timestamp)
        {
            return From.CompareTo(timestamp) <= 0 && To.CompareTo(timestamp) >= 0;
        }
    }
}