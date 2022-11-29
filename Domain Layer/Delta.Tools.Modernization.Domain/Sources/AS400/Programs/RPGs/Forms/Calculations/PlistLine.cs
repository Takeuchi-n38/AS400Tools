using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class PlistLine : CalculationLine, ISingleStatement
    {
        public bool IsEntry => Factor1.Trim() == "*ENTRY";

        public PlistLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
/*
     C     *ENTRY        PLIST
     C                   PARM                    P\DATAID          2
     C                   PARM                    P\KBKB            3
 */
