using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class SndbrkmsgLine : CLLine
    {
        public readonly string Msg;
        public readonly string Tomsgq;
        public SndbrkmsgLine(string aMsg, string aTomsgq, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.Msg = aMsg;
            this.Tomsgq = aTomsgq;
        }

    }
}
