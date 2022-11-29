using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Sources.AS400.Programs.RPGs.Forms.Calculations.Dos
{
    public class DoLine : CalculationLine, INestedBlockStartStatement
    {

        public DoLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
