using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DowgtLine : CalculationLine, INestedBlockStartStatement
    {

        public DowgtLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
