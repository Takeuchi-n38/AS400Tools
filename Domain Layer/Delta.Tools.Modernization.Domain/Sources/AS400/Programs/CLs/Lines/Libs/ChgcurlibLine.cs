using Delta.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines.Libs
{
    public class ChgcurlibLine : CLLine
    {
        public readonly Library CURLIB;

        public ChgcurlibLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            CURLIB = library;
        }
        //CHGCURLIB  CHGCURLIB  CURLIB(LFBLIB) CHGCURLIB YTSTLIB

    }
}
