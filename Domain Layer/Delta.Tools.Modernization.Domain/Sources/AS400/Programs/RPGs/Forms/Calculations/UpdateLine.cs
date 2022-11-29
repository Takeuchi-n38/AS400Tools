using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class UpdateLine : CalculationLine, ISingleStatement
    {

        public UpdateLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
