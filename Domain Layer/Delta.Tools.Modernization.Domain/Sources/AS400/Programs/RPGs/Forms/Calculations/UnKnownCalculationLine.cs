using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class UnKnownCalculationLine : CalculationLine, ISingleStatement
    {
        public UnKnownCalculationLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
