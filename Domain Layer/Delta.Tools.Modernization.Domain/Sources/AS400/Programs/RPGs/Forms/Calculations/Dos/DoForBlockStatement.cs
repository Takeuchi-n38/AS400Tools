using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DoForBlockStatement : NestedBlockStatement<IStatement>
    {
        public readonly DoForLine openerLine;

        private DoForBlockStatement(DoForLine openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is DoForLine)
            {
                result = new DoForBlockStatement((DoForLine)openerLine);
                return true;
            }

            result = null;
            return false;
        }

        public CalculationLine closerLine;
        public override bool TryClose(IStatement closerLine)
        {
            if (closerLine is EnddoLine)
            {
                this.closerLine = (EnddoLine)closerLine;
                return true;
            }
            if (closerLine is EndLine)
            {
                this.closerLine = (EndLine)closerLine;
                return true;
            }
            return false;
        }

    }
}
