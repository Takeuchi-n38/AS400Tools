using Delta.Tools.AS400.DDSs;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class DefineLine : CalculationLine, ISingleStatement
    {

        public DefineLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
            //0088      C     *LIKE         DEFINE    SWQTY         WKQTY
