using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class IterLine : CalculationLine, ISingleStatement
    {

        public IterLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
