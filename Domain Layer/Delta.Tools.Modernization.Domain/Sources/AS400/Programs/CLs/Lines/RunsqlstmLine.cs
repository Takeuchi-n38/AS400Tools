using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class RunsqlstmLine : CLLine
    {
        public readonly string Srcfile;
        public readonly string Srcmbr;
        public RunsqlstmLine(string Srcfile, string Srcmbr, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.Srcfile = Srcfile;
            this.Srcmbr = Srcmbr;
        }
        //RUNSQLSTM  SRCFILE(YAGILIB/QSQLSRC) SRCMBR(DSJNAJG) COMMIT(*NONE)

    }
}
