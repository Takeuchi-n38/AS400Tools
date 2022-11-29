using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class MoveaLine : CalculationLine, ISingleStatement
    {

        public MoveaLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
////0744      C                   MOVEA     '100'         *IN(71)