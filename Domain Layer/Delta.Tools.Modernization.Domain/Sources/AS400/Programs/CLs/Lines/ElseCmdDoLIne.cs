using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class ElseCmdDoLIne : CLLine
    {
        public ElseCmdDoLIne(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex, originalLineStartIndex)
        {

        }
    }
    //ELSE       CMD(DO)
}
