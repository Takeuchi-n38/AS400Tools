using Delta.AS400.Libraries;
using System.Collections.Generic;

namespace Delta.Tools.AS400.Programs.CLs.Lines.Libs
{
    public class ChgliblLine : CLLine
    {
        public readonly Library CURLIB;
        public readonly List<Library> LIBL;
        public ChgliblLine(Library CURLIB, List<Library> Libraries, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.CURLIB = CURLIB;
            LIBL = Libraries;
        }

    }
}
//CHGLIBL    LIBL(QEVX QGPL QTEMP CDD0001 EIGYLIB RJELIB WRKLIB) CURLIB(SALELIB)