﻿using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs
{
    public class IfltLine : CalculationLine, INestedBlockStartStatement
    {

        public IfltLine(CalculationLine RPGCLine) : base(RPGCLine)
        {
        }

    }
}
