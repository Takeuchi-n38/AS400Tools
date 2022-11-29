using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Selects
{
    public class WheqLine : CalculationLine, ISingleStatement
    {

        public WheqLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
