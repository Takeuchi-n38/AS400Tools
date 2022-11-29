using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class FtpLine : CLLine
    {
        public readonly string Rmtsys;
        public FtpLine(string Rmtsys,string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.Rmtsys= Rmtsys;
        }
    }
}
//FTP RMTSYS(&TCPADDR)