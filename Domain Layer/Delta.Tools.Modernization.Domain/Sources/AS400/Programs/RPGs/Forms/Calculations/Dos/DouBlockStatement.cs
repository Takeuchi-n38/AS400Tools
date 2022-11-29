using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DouBlockStatement : NestedBlockStatement<IStatement>
    {
        public DouLine openerLine;

        private DouBlockStatement(DouLine openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is DouLine)//DouLine
            {
                result = new DouBlockStatement((DouLine)openerLine);
                return true;
            }

            result = null;
            return false;
        }


        public EnddoLine closerLine;
        public override bool TryClose(IStatement closerLine)
        {
            if (closerLine is EnddoLine)
            {
                this.closerLine = (EnddoLine)closerLine;
                return true;
            }
            return false;
        }

    }
}
