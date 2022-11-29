using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.Ifs;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs
{
    public class IfBlockStatement3 : NestedBlockStatement<IStatement>
    {
        public INestedBlockStartStatement openerLine;

        private IfBlockStatement3(INestedBlockStartStatement openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is IfeqLine || openerLine is IfneLine || openerLine is IfleLine || openerLine is IfgeLine || openerLine is IfltLine || openerLine is IfgtLine)
            {
                result = new IfBlockStatement3((INestedBlockStartStatement)openerLine);
                return true;
            }

            result = null;
            return false;
        }

        public IIfBlockEndStatement closerLine;
        public override bool TryClose(IStatement closerLine)
        {
            if (closerLine is IIfBlockEndStatement)
            {
                this.closerLine = (IIfBlockEndStatement)closerLine;
                return true;
            }
            return false;
        }

        public static bool IsContent(IStatement l) => !(l is IfeqLine || l is IfneLine || l is IfleLine || l is IfgeLine || l is IfltLine || l is IfgtLine
            || l is OreqLine || l is OrneLine || l is OrltLine || l is OrgeLine || l is OrgtLine || l is AndeqLine || l is AndgtLine || l is AndltLine || l is AndgeLine || l is AndleLine || l is AndneLine || l is ICommentStatement);

    }
}
