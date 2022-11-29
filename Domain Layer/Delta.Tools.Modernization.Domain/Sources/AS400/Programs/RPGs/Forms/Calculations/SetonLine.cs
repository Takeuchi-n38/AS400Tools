using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class SetonLine : CalculationLine, ISingleStatement
    {

        public SetonLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}