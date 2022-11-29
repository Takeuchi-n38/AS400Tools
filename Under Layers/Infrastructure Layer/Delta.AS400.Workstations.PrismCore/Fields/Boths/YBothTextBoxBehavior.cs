using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.AS400.Workstations.Fields.Boths
{
    public class YBothTextBoxBehavior : NumericBothTextBoxBehavior
    {
        protected override bool signed => false;

    }
}
