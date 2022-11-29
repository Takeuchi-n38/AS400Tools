using Delta.AS400.Libraries;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.AS400.RPGs.Forms.Calculations
{
    interface ICalculationLineFactory
    {
        CalculationLine Create(Library library, CalculationLine rpgCline);
    }
}
