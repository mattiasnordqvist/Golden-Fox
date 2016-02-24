namespace GoldenFox.Internal
{
    internal class Between
    {
        public Timestamp From { get; set; }

        public Timestamp To { get; set; }

        public Between(Timestamp from, Timestamp to)
        {
            From = @from;
            To = to;
        }

        public bool Contains(Timestamp timestamp)
        {
            return From.CompareTo(timestamp) <= 0 && To.CompareTo(timestamp) >= 0;
        }
    }
}