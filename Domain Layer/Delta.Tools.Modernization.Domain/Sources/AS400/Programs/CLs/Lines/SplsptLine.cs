using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class SplsptLine : CLLine
    {

        public SplsptLine(string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {

        }
    }
}
//SPLSPT SPLF(JVAA10MK08) OUTQ(PDFKOUBAI)
