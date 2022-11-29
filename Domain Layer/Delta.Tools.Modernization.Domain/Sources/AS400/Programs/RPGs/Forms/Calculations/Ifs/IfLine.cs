using Delta.Tools.Sources.Statements.Blocks.Ifs;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs
{
    public class IfLine : CalculationLine, IIfBlockStartStatement
    {

        public IfLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
