using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class EvalLine : CalculationLine, ISingleStatement
    {

        public EvalLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
