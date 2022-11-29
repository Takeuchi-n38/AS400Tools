using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Selects
{
    public class SelectBlock : NestedBlockStatement<IStatement>
    {
        public INestedBlockStartStatement openerLine;

        private SelectBlock(INestedBlockStartStatement openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is SelectLine)
            {
                result = new SelectBlock((SelectLine)openerLine);
                return true;
            }

            result = null;
            return false;
        }


        public EndslLine closerLine;
        public override bool TryClose(IStatement closerLine)
        {
            if (closerLine is EndslLine)
            {
                this.closerLine = (EndslLine)closerLine;
                return true;
            }
            return false;
        }

    }
}
