using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class MovelLine : CalculationLine, ISingleStatement
    {
        public MovelLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
//1237      C                   MOVEL     WLINE         WLIN1   