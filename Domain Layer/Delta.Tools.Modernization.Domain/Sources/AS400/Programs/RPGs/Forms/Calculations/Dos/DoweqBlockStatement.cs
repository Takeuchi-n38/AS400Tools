using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DoweqBlockStatement : NestedBlockStatement<IStatement>
    {
        public DoweqLine openerLine;

        private DoweqBlockStatement(DoweqLine openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is DoweqLine doueqLine)
            {
                result = new DoweqBlockStatement(doueqLine);
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
