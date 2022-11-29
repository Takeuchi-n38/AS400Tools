using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class MoveLine : CalculationLine, ISingleStatement
    {

        public MoveLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
//0738      C                   MOVE      '0'           *IN92          