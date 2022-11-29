using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Sources.AS400.Programs.RPGs.Forms.Calculations
{
    public class CatLine : CalculationLine, ISingleStatement
    {

        public CatLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
//0289   :: C           RECID     CAT  VCDW      MSG3      P                 
//0290   :: C                     CAT  HIN       MSG3                        
