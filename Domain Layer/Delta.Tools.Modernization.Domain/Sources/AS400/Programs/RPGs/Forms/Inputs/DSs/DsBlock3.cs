using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.DSs
{
    public class DsBlock3 : NestedBlockStatement<IStatement>
    {
        public IRPGInputLine3 openerLine;

        private DsBlock3(DsLine3 openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement targetOpenerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (targetOpenerLine is DsLine3)
            {
                result = new DsBlock3((DsLine3)targetOpenerLine);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TryClose(IStatement closerLine)
        {
            return closerLine is INestedBlockStartStatement || (closerLine is ICommentStatement && Statements.Count()>=1);
        }

    }
}
