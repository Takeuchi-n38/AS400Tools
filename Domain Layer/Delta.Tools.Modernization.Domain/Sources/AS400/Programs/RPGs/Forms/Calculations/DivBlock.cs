using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class DivBlock : NestedBlockStatement<IStatement>
    {
        public DivLine openerLine;

        private DivBlock(DivLine openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is DivLine)
            {
                result = new DivBlock((DivLine)openerLine);
                return true;
            }

            result = null;
            return false;
        }
        public override bool Skip => this.closerLine == null;

        public MvrLine? closerLine;
        public override bool TryClose(IStatement closerLine)
        {
            if (closerLine is MvrLine)
            {
                this.closerLine = (MvrLine)closerLine;
                return true;
            }
            else
            {
                this.closerLine = null;
                return true;
            }
        }


    }
}
//todo cLine //1635      C     WYY           DIV       4             SYO               4 0      
//todo cLine //1636      C                   MVR                     AMARI             4 0      
