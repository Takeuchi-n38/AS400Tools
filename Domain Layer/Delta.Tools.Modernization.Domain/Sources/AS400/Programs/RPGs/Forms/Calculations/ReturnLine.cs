using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class ReturnLine : CalculationLine, ISingleStatement
    {

        public ReturnLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
