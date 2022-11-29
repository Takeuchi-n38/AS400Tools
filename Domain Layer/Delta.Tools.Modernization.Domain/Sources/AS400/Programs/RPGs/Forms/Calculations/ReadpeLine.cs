using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class ReadpeLine : CalculationLine, ISingleStatement
    {

        public ReadpeLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
