using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Types.Dates
{
    public class Week
    {
        public readonly int yyMMw;

        Week(int ayyMMw)
        {
            yyMMw = ayyMMw;
        }

        public static Week Of(int ayyMMw) => new Week(ayyMMw);

    }
}
