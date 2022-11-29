using Delta.Tools.AS400.DDSs;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class KfldLine : CalculationLine, ISingleStatement
    {

        public KfldLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}