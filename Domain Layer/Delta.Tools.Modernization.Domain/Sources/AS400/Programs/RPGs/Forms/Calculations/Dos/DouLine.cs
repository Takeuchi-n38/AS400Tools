using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DouLine : CalculationLine, INestedBlockStartStatement
    {

        public DouLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
//0355      C                   DOU       (*INKC='1') OR (*INKD='1') OR(D1OPT='4') AND (*INKF='1')

