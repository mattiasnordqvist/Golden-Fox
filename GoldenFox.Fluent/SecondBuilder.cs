using GoldenFox.Internal.Operators.Intervals;

namespace GoldenFox.Fluent
{
    public class SecondBuilder : Every
    {
        
        internal override OperatorBuilder InternalBuild()
        {
            var second = new Second();
            return new OperatorBuilder(constraints =>
            {
                second.AddConstraints(constraints);
                return second;
            });
        }
    }
}