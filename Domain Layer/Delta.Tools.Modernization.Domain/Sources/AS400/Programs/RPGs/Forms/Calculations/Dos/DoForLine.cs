using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DoForLine : CalculationLine, INestedBlockStartStatement
    {

        public DoForLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

        //0294      C     1             DO        15            IX           

    }
}
