using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class DeleteLine : CalculationLine, ISingleStatement
    {

        public DeleteLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
