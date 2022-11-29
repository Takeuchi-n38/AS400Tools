using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DoueqLine : CalculationLine, INestedBlockStartStatement
    {

        public DoueqLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
//0060      C           *INLR     DOUEQ*ON          