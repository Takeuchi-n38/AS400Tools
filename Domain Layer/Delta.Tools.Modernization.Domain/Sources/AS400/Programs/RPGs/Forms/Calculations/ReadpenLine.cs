using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class ReadpenLine : CalculationLine, ISingleStatement
    {
        public ReadpenLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
