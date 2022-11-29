using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class CompLine : CalculationLine, ISingleStatement
    {

        public CompLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}
/*
 高 (HI): (71 から 72) 演算項目 1 が演算項目 2 より大きい。
低 (LO): (73 から 74) 演算項目 1 が演算項目 2 より小さい。
等 (EQ): (75 から 76) 演算項目 1 が演算項目 2 と等しい。
 */