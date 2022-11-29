using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class DivLine : CalculationLine, INestedBlockStartStatement
    {

        public DivLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
