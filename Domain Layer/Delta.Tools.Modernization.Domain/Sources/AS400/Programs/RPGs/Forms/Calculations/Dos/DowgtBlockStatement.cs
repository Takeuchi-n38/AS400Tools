using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DowgtBlockStatement : NestedBlockStatement<IStatement>
    {
        public DowgtLine openerLine;

        private DowgtBlockStatement(DowgtLine openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is DowgtLine doueqLine)
            {
                result = new DowgtBlockStatement(doueqLine);
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
