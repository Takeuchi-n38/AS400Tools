using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class ExcptLine : CalculationLine, ISingleStatement
    {

        public ExcptLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
//0318      C                   EXCEPT    #HED    