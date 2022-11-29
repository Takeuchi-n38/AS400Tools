using Delta.Tools.Sources.Statements.Blocks.Ifs;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs
{
    public class EndLine : CalculationLine, IIfBlockEndStatement
    {
        public EndLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }
    }
}
