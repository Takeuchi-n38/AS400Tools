using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class SubLine : CalculationLine, ISingleStatement
    {

        public SubLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
