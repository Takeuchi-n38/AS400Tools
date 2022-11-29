using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class DltovrLine : CLLine
    {
        public DltovrLine(string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {

        }
    }
}
//DLTOVR     FILE(*ALL)