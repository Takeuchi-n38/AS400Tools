using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class MultLine : CalculationLine, ISingleStatement
    {

        public MultLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
//1372      C     WMONEY        MULT      1             WKMONEY   