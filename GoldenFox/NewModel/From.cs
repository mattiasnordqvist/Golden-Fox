using System;

namespace GoldenFox.NewModel
{
    public class From
    {
        private readonly DateTime _from;

        public From(DateTime @from)
        {
            _from = @from;
        }

        public DateTime Evaluate()
        {
            return _from;
        }
    }
}