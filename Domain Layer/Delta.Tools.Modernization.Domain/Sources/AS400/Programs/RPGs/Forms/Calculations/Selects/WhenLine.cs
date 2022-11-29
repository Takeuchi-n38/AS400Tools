using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Selects
{
    public class WhenLine : CalculationLine, ISingleStatement
    {

        public WhenLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
//0299      C                   WHEN      (D1CUST<>H1CUST) OR (D1CUST1<>H1CUST1)   