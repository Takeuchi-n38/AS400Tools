using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class SetofLine : CalculationLine, ISingleStatement
    {

        public SetofLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
