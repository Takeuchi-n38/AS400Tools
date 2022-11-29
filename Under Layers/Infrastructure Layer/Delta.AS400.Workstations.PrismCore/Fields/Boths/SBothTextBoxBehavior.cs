using System.Linq;

namespace Delta.AS400.Workstations.Fields.Boths
{
    public class SBothTextBoxBehavior : NumericBothTextBoxBehavior
    {
        protected override bool signed => true;

    }
}
