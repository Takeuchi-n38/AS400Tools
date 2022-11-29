using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Sources.AS400.Programs.RPGs.Forms.Calculations
{
    public class TestnLine : CalculationLine, ISingleStatement
    {

        public TestnLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
//0228      C                     TESTN          KDB        90
