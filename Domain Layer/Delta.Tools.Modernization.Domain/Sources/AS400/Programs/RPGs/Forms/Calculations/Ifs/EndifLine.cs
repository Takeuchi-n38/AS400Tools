using Delta.Tools.Sources.Statements.Blocks.Ifs;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs
{
    public class EndifLine : CalculationLine, IIfBlockEndStatement
    {

        public EndifLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
