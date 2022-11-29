using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class CaseqLine : CalculationLine, ISingleStatement
    {

        public CaseqLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
//0110      C     *IN12         CASEQ     '1'           @BOT