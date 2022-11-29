using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Lines
{
    public class RPGCommentLine : Line, IRPGLine, ICommentStatement
    {
        readonly FormType FormType;
        FormType IRPGLine.FormType => FormType;

        public RPGCommentLine(FormType FormType,string value, int originalLineStartIndex) : base(value.Length < 76 ? value.PadRight(76) : value, originalLineStartIndex)
        {
            this.FormType=FormType;
        }

    }
}
