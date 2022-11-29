using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dss
{
    public class DsBlock4 : NestedBlockStatement<IStatement>
    {
        public IRPGDefinitionLine4 openerLine;

        private DsBlock4(DsLine4 openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement targetOpenerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (targetOpenerLine is DsLine4)
            {
                result = new DsBlock4((DsLine4)targetOpenerLine);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TryClose(IStatement closerLine)
        {
            return closerLine is INestedBlockStartStatement || closerLine is ICommentStatement || closerLine is VarLine;
        }

    }
}
