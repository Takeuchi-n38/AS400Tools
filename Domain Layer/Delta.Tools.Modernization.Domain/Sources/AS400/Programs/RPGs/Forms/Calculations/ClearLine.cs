using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class ClearLine : CalculationLine, ISingleStatement
    {

        public ClearLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
