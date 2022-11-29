using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs
{
    public class IfgtLine : CalculationLine, INestedBlockStartStatement
    {

        public IfgtLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
