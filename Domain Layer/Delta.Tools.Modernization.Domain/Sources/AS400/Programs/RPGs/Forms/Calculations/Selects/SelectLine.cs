using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Selects
{
    public class SelectLine : CalculationLine, INestedBlockStartStatement
    {

        public SelectLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
